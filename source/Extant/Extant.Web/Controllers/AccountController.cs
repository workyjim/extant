//-----------------------------------------------------------------------
// <copyright file="AccountController.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Extant.Data;
using Extant.Data.Entities;
using Extant.Data.Repositories;
using Extant.Web.Infrastructure;
using Extant.Web.Models;

namespace Extant.Web.Controllers
{
    public class AccountController : MembershipController
    {

        private readonly IDiseaseAreaRepository DiseaseAreaRepo;
        private readonly IUserRepository UserRepo;
        private readonly IMailer Mailer;

        public AccountController(IDiseaseAreaRepository diseaseAreaRepo, IUserRepository userRepo, IMailer mailer)
        {
            DiseaseAreaRepo = diseaseAreaRepo;
            UserRepo = userRepo;
            Mailer = mailer;
        }

        public ActionResult LogOn()
        {
            if ( TempData.ContainsKey("reset"))
            {
                ModelState.AddModelError("reset", TempData["reset"].ToString());
            }
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(string username, string password, string returnUrl)
        {
            if (Membership.ValidateUser(username, password))
            {
                var user = Membership.GetUser(username);
                if ( !user.IsApproved )
                {
                    ModelState.AddModelError("_FORM", "This user account has not been approved. Please contact the system administrator.");
                }
                else if (user.IsLockedOut)
                {
                    ModelState.AddModelError("_FORM", "This user account is locked out. Please contact the system administrator.");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("_FORM", "The email address or password provided is incorrect.");
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        [HttpGet]
        public ActionResult Reauthenticate()
        {
            // This action is called from a javascript login timeout counter in the browser.
            // It is called before the actual ASP.NET Forms authentication token expires.
            // So we need to explicitly sign the user out - otherwise they can hit the back button
            // and will still have an active Forms authentication token.
            // For details of Forms Authentication tickets and cookies see 
            // the 'The forms authentication ticket times out' section here:
            //            http://support.microsoft.com/kb/910439
            FormsAuthentication.SignOut();
            return View();
        }

        [HttpPost]
        public ActionResult Reauthenticate(string username, string password)
        {
            if (Membership.ValidateUser(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return new EmptyResult();
            }
            throw new Exception();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            var model = new RegisterModel();
            model.DiseaseAreas = DiseaseAreaRepo.GetAllPublished();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var diseaseArea = DiseaseAreaRepo.Get(model.DiseaseAreaId);
                if (null == diseaseArea)
                {
                    ModelState.AddModelError("DiseaseAreaId", "Invalid disease area");
                }
                else
                {
                    // Attempt to register the user
                    MembershipCreateStatus createStatus;

                    var membershipUser = Membership.CreateUser(model.Name, model.Password, model.Email, null, null, false, null, out createStatus);
                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        var newUser = UserRepo.Get((int)membershipUser.ProviderUserKey);
                        newUser.DiseaseAreas.Add(diseaseArea);
                        newUser.EmailValidated = false;
                        newUser.EmailValidationCode = SaltedHash.GetUrlEncodedBytes(16);
                        UserRepo.Save(newUser);

                        SendRegistrationValidationEmail(newUser);

                        return View("RegisterSuccess");
                    }
                    else
                    {
                        ErrorCodeToModelError(createStatus);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            model.DiseaseAreas = DiseaseAreaRepo.GetAllPublished();
            return View(model);
        }

        private void SendRegistrationValidationEmail(User user)
        {
            //send a validation email to the user to prove they own the address with which they registered.

            var uri = Regex.Match(HttpContext.Request.Url.AbsoluteUri, @"^(http|https)://[a-zA-Z0-9\-\.]+(:[0-9]*)?/").Value;

            var resetUrl = uri + "Account/ValidateEmail?code=" + user.EmailValidationCode;

            string subject = ConfigurationManager.AppSettings["OrganisationName"]+" "+ConfigurationManager.AppSettings["CatalogueName"] + " - User Registration";

            var body = "Your email address has been used to apply for access to the " + ConfigurationManager.AppSettings["OrganisationName"] + " " + ConfigurationManager.AppSettings["CatalogueName"] + " web site.\n\n" +
                    uri + "\n\n" +
                    "If you you did not register for access to this site then please ignore this email.\n\n" +
                    "To proceed with registration please click the link below (or paste it into your browser):\n\n" +
                    resetUrl;

            Mailer.Send(user.Email, subject, body);
        }


        [HttpGet]
        public ActionResult ValidateEmail(string code)
        {
            var user = UserRepo.GetUserByEmailValidationCode(code);

            if (user!=null)
            {
                user.EmailValidated = true;

                var autoApprove = Regex.IsMatch(user.Email, @"(\.ac\.uk|\.nhs\.net|\.nhs\.uk)$", RegexOptions.IgnoreCase);
                if (autoApprove)
                {
                    user.IsApproved = true;
                }
                else if(!user.IsApproved)
                {
                    //send email to admin users
                    var diseaseArea = user.DiseaseAreas.First();
                    var uri = Regex.Match(HttpContext.Request.Url.AbsoluteUri, @"^(http|https)://[a-zA-Z0-9\-\.]+(:[0-9]*)?/").Value
                        + "Admin/Users";
                    string subject = ConfigurationManager.AppSettings["OrganisationName"] + " " + ConfigurationManager.AppSettings["CatalogueName"] + " - User Registration";
                    var body =
                        string.Format(
                            "A new user has registered to use the "+ConfigurationManager.AppSettings["OrganisationName"] + " " + ConfigurationManager.AppSettings["CatalogueName"]+".\n\n" +
                            "Name = {0}\n" +
                            "Email = {1}\n" +
                            "Disease Area = {2}\n\n" +
                            "Please click the following link to approve their account: {3}\n\n",
                            user.UserName, user.Email, diseaseArea.DiseaseAreaName, uri);
                    foreach (var toEmail in UserRepo.GetEmailsForRegistrationNotification(diseaseArea.Id))
                    {
                        Mailer.Send(toEmail.Email, subject, body);
                    }                    
                }

                UserRepo.Save(user);
                
                return View("ValidateEmailSuccess", user.IsApproved);
            }

            return View("ValidateEmailFailed");
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Details(IPrincipal identity)
        {
            var user = UserRepo.GetByEmail(identity.Identity.Name);
            var model = Mapper.Map<User, UserModel>(user);
            model.AllDiseaseAreas =
                Mapper.Map<IEnumerable<DiseaseArea>, IEnumerable<DiseaseAreaBasicModel>>(DiseaseAreaRepo.GetAllPublished());
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Details(UserModel model, IPrincipal identity)
        {
            if ( ModelState.IsValid)
            {
                //check for unique email
                var user = UserRepo.GetByEmail(identity.Identity.Name);
                var otherUser = UserRepo.GetByEmail(model.Email);
                if ( null != otherUser && user.Id != otherUser.Id )
                {
                    ErrorCodeToModelError(MembershipCreateStatus.DuplicateEmail);
                }
                else
                {
                    user.UserName = model.UserName;

                    foreach (var da in user.DiseaseAreas.Where(da => !model.DiseaseAreas.Contains(da.Id)).ToList())
                    {
                        user.DiseaseAreas.Remove(da);
                    }
                    foreach (var daid in model.DiseaseAreas.Where(daid => !user.DiseaseAreas.Select(da => da.Id).Contains(daid)))
                    {
                        user.DiseaseAreas.Add(DiseaseAreaRepo.Get(daid));
                    }

                    user.Email = model.Email;
                    UserRepo.Save(user);
                    if (!string.Equals(user.Email, identity.Identity.Name))
                    {
                        FormsAuthentication.SignOut();
                        FormsAuthentication.SetAuthCookie(model.Email, false);
                    }
                    return RedirectToAction("DetailsSuccess");
                }
            }
            model.AllDiseaseAreas =
                Mapper.Map<IEnumerable<DiseaseArea>, IEnumerable<DiseaseAreaBasicModel>>(DiseaseAreaRepo.GetAllPublished());
            return View(model);
        }

        public ActionResult ResetPassword(string email)
        {
            var muser = Membership.GetUser(email);
            if ( null == muser )
            {
                TempData.Add("reset", "Unable to reset password - no user exists for the email entered");
                return RedirectToAction("LogOn");
            }
            var hash = muser.GetPassword();
            var domain = ControllerContext.HttpContext.Request.Url.AbsoluteUri;
            var resetUrl = domain.Replace("ResetPassword", "ResetPasswordStep2") + "?code=" + hash;

            Mailer.Send(
                email,
                ConfigurationManager.AppSettings["OrganisationName"] + " " + ConfigurationManager.AppSettings["CatalogueName"] + " - Reset Password", 
                "To reset your password click the following link (or paste it into your browser):\n\n"+resetUrl+
                "\n\nPlease note that this link is only valid for the next 30 minutes.");
            return RedirectToAction("ResetPasswordStep1");
        }

        public ActionResult ResetPasswordStep1()
        {
            return View();
        }

        public ActionResult ResetPasswordStep2(string code)
        {
            return View(new ResetPasswordModel{Code = code});
        }

        [HttpPost]
        public ActionResult ResetPasswordStep2(ResetPasswordModel model)
        {
            if ( ModelState.IsValid )
            {
                var resetOk = UserRepo.CheckPasswordReset(model.Email, model.Code);
                if ( resetOk )
                {
                    var muser = Membership.GetUser(model.Email);
                    muser.ResetPassword(model.Password);
                    return RedirectToAction("ResetPasswordSuccess");
                }
                ModelState.AddModelError("_FORM", "Your password could not be reset. Either the reset password link is invalid or it has expired.");
            }
            return View(model);
        }

        public ActionResult ResetPasswordSuccess()
        {
            return View();
        }

        public ActionResult DetailsSuccess()
        {
            return View();
        }

    }
}
