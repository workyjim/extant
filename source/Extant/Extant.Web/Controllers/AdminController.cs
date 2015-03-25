//-----------------------------------------------------------------------
// <copyright file="AdminController.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
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
    [AjaxAuthorize(Roles=Constants.AdministratorRole+","+Constants.HubLeadRole)]
    public class AdminController : MembershipController
    {
        private readonly IStudyRepository StudyRepo;
        private readonly IDiseaseAreaRepository DiseaseAreaRepo;
        private readonly IUserRepository UserRepo;
        private readonly IRoleRepository RoleRepo;
        private readonly IDataItemRepository DataItemRepo;
        private readonly IMailer Mailer;

        public AdminController(IStudyRepository studyRepo, IDiseaseAreaRepository diseaseAreaRepo,
            IDataItemRepository dataItemRepo, IUserRepository userRepo, IRoleRepository roleRepo, 
            IMailer mailer)
        {
            StudyRepo = studyRepo;
            DiseaseAreaRepo = diseaseAreaRepo;
            UserRepo = userRepo;
            RoleRepo = roleRepo;
            DataItemRepo = dataItemRepo;
            Mailer = mailer;
        }

        [HttpGet]
        public ActionResult Index(IPrincipal principal)
        {
            return View(principal.IsInRole(Constants.AdministratorRole));
        }

        #region User Management

        public ActionResult Users(IPrincipal principal)
        {
            var users = principal.IsInRole(Constants.AdministratorRole) 
                ? UserRepo.GetNotDeleted()
                : UserRepo.GetUsersInDiseaseAreas(UserRepo.GetByEmail(principal.Identity.Name).DiseaseAreas.Select(da => da.Id).ToArray());
;
            return View(Mapper.Map<IEnumerable<User>, IEnumerable<AdminUserModel>>(users.OrderBy(u => u.UserName)));
        }

        [HttpPost]
        [HubLeadUserAccessControl]
        public ActionResult ApproveUser(int _id)
        {
            var user = Membership.GetUser(_id);
            user.IsApproved = true;
            Membership.UpdateUser(user);
            SendAccountApprovedEmail(user.Comment, user.Email);
            return new EmptyResult();
        }

        [HttpPost]
        [HubLeadUserAccessControl]
        public ActionResult ValidateEmail(int _id)
        {
            var user = UserRepo.Get(_id);
            user.EmailValidated = true;
            UserRepo.Save(user);
            return new EmptyResult();
        }

        [HttpPost]
        [HubLeadUserAccessControl]
        public ActionResult UnlockUser(int _id)
        {
            var user = Membership.GetUser(_id);
            user.UnlockUser();
            SendAccountUnlockedEmail(user.Comment, user.Email);
            return new EmptyResult();
        }

        [HttpPost]
        [HubLeadUserAccessControl]
        public ActionResult DeleteUser(int _id)
        {
            var user = Membership.GetUser(_id);
            Membership.DeleteUser(user.UserName);
            return new EmptyResult();
        }

        [HubLeadUserAccessControl]
        public ActionResult EditUser(int _id, IPrincipal principal)
        {
            var user = UserRepo.Get(_id);
            var model = Mapper.Map<User, EditUserModel>(user);
            model.AllDiseaseAreas =
                Mapper.Map<IEnumerable<DiseaseArea>, IEnumerable<DiseaseAreaBasicModel>>(DiseaseAreaRepo.GetAllPublished());
            model.HasAdminRole = principal.IsInRole(Constants.AdministratorRole);
            return View(model);
        }

        [HttpPost]
        [HubLeadUserAccessControl]
        public ActionResult EditUser(int _id, EditUserModel model, IPrincipal principal)
        {
            if ( ModelState.IsValid )
            {
                var user = UserRepo.Get(_id);
                user.UserName = model.UserName;
                user.Email = model.Email;

                foreach ( var da in user.DiseaseAreas.Where(da => !model.DiseaseAreas.Contains(da.Id)).ToList() )
                {
                    user.DiseaseAreas.Remove(da);
                }
                foreach (var daid in model.DiseaseAreas.Where(daid => !user.DiseaseAreas.Select(da => da.Id).Contains(daid)))
                {
                    user.DiseaseAreas.Add(DiseaseAreaRepo.Get(daid));
                }

                if (principal.IsInRole(Constants.AdministratorRole))
                {
                    //Only full administrator can assign roles
                    if (model.IsAdministrator &&
                        !user.Roles.Select(r => r.RoleName).Contains(Constants.AdministratorRole))
                        user.Roles.Add(RoleRepo.GetByName(Constants.AdministratorRole));
                    if (!model.IsAdministrator &&
                        user.Roles.Select(r => r.RoleName).Contains(Constants.AdministratorRole))
                        user.Roles.Remove(RoleRepo.GetByName(Constants.AdministratorRole));
                    if (model.IsHubLead && !user.Roles.Select(r => r.RoleName).Contains(Constants.HubLeadRole))
                        user.Roles.Add(RoleRepo.GetByName(Constants.HubLeadRole));
                    if (!model.IsHubLead && user.Roles.Select(r => r.RoleName).Contains(Constants.HubLeadRole))
                        user.Roles.Remove(RoleRepo.GetByName(Constants.HubLeadRole));
                }
                UserRepo.Save(user);

                if (!string.IsNullOrEmpty(model.Password))
                {
                    var muser = Membership.GetUser(model.Email);
                    muser.ResetPassword(model.Password);
                }
                return RedirectToAction("EditUserSuccess");
            }
            model.AllDiseaseAreas =
                Mapper.Map<IEnumerable<DiseaseArea>, IEnumerable<DiseaseAreaBasicModel>>(DiseaseAreaRepo.GetAllPublished());
            return View(model);
        }

        public ActionResult EditUserSuccess()
        {
            return View();
        }

        public ActionResult AddUser(IPrincipal principal)
        {
            var diseaseAreas = principal.IsInRole(Constants.AdministratorRole)
                                   ? DiseaseAreaRepo.GetAllPublished()
                                   : UserRepo.GetByEmail(principal.Identity.Name).DiseaseAreas;
            var model = new AddUserModel
                            {
                                AllDiseaseAreas = Mapper.Map<IEnumerable<DiseaseArea>, IEnumerable<DiseaseAreaBasicModel>>(diseaseAreas)
                            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddUser(AddUserModel model, IPrincipal principal)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                var membershipUser = Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);
                if (createStatus == MembershipCreateStatus.Success)
                {
                    //set disease area
                    var newUser = UserRepo.Get((int)membershipUser.ProviderUserKey);
                    var das = DiseaseAreaRepo.Get(model.DiseaseAreas);
                    foreach (var da in das)
                    {
                        newUser.DiseaseAreas.Add(da);
                    }

                    if (principal.IsInRole(Constants.AdministratorRole))
                    {
                        //Only a full administrator can set roles
                        if (model.IsAdministrator)
                            newUser.AddRole(RoleRepo.GetByName(Constants.AdministratorRole));
                        if (model.IsHubLead)
                            newUser.AddRole(RoleRepo.GetByName(Constants.HubLeadRole));
                    }

                    UserRepo.Save(newUser);
                    return RedirectToAction("AddUserSuccess");
                }
                else
                {
                    ErrorCodeToModelError(createStatus);
                }
            }

            var diseaseAreas = principal.IsInRole(Constants.AdministratorRole)
                                   ? DiseaseAreaRepo.GetAllPublished()
                                   : UserRepo.GetByEmail(principal.Identity.Name).DiseaseAreas;
            model.AllDiseaseAreas =
                Mapper.Map<IEnumerable<DiseaseArea>, IEnumerable<DiseaseAreaBasicModel>>(diseaseAreas);
            model.HasAdminRole = principal.IsInRole(Constants.AdministratorRole);
            return View(model);
        }

        public ActionResult AddUserSuccess()
        {
            return View();
        }

        #endregion

        #region Disease Areas

        [HttpGet]
        public ActionResult DiseaseAreas(IPrincipal principal)
        {
            var das = principal.IsInRole(Constants.AdministratorRole)
                ? DiseaseAreaRepo.GetAll()
                : UserRepo.GetByEmail(principal.Identity.Name).DiseaseAreas;
            return View(new DiseaseAreasModel
                            {
                                IsAdmin = principal.IsInRole(Constants.AdministratorRole),
                                DiseaseAreas =
                                    Mapper.Map<IEnumerable<DiseaseArea>, IEnumerable<DiseaseAreaBasicModel>>(das)
                            });
        }

        [HttpGet]
        [HubLeadDiseaseAreaAccessControl]
        public ActionResult DiseaseArea(int _id)
        {
            var da = DiseaseAreaRepo.Get(_id);
            return View(Mapper.Map<DiseaseArea, DiseaseAreaModel>(da));
        }

        [HttpPost]
        [HubLeadDiseaseAreaAccessControl]
        public ActionResult DiseaseArea(int _id, string synonyms, IEnumerable<CategoryModel> categories, bool? published)
        {
            var diseaseArea = DiseaseAreaRepo.Get(_id);
            diseaseArea.DiseaseAreaSynonyms = synonyms;
            RebuildCategories(diseaseArea.Categories, categories);
            diseaseArea.Published = published ?? false;
            DiseaseAreaRepo.Save(diseaseArea);
            if ( HttpContext.Request.IsAjaxRequest() )
            {
                return new EmptyResult();
            }
            return RedirectToAction("DiseaseAreas");
        }

        [HttpPost]
        [HubLeadDiseaseAreaAccessControl]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult UpdateDiseaseAreaName(int _id, string new_value)
        {
            var diseaseArea = DiseaseAreaRepo.Get(_id);
            diseaseArea.DiseaseAreaName = new_value;
            DiseaseAreaRepo.Save(diseaseArea);
            return Json(new { is_error = false, error_text = "", html = new_value }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAuthorize(Roles=Constants.AdministratorRole)]
        public ActionResult AddDiseaseArea(string daname, IPrincipal identity)
        {
            var da = new DiseaseArea {DiseaseAreaName = daname};
            da = DiseaseAreaRepo.Save(da);
            return RedirectToAction("DiseaseArea", new {_id = da.Id});
        }

        #endregion

        #region Search Index

        [AjaxAuthorize(Roles = Constants.AdministratorRole)]
        public ActionResult RebuildSearchIndex()
        {
            StudyRepo.RebuildSearchIndex();
            DataItemRepo.RebuildSearchIndex();
            return View();
        }

        #endregion

        private void RebuildCategories(IList<Category> categories, IEnumerable<CategoryModel> modelCategories)
        {
            if (null == modelCategories || !modelCategories.Any())
            {
                categories.Clear();
            }
            else
            {
                var deletedCategories =
                    categories.Where(c => !modelCategories.Select(mc => mc.Id).Contains(c.Id)).ToList();
                foreach (var cat in deletedCategories)
                {
                    categories.Remove(cat);
                }

                foreach (var mc in modelCategories.Select((x, i) => new { Index = i, Category = x }).OrderBy(x => x.Index)
                    )
                {
                    var cat = categories.Where(c => c.Id != 0 && c.Id == mc.Category.Id).SingleOrDefault();
                    if (null == cat)
                    {
                        cat = new Category();
                    }
                    else
                    {
                        categories.Remove(cat);
                    }
                    cat.CategoryName = mc.Category.CategoryName;
                    if (mc.Index < categories.Count)
                        categories.Insert(mc.Index, cat);
                    else
                        categories.Add(cat);

                    RebuildCategory(cat, mc.Category);
                }
            }
        }

        private void RebuildCategory(Category category, CategoryModel modelCategory)
        {
            if (null == modelCategory.DataItems || !modelCategory.DataItems.Any())
            {
                category.DataItems.Clear();
            }
            else
            {
                var deletedDataItems =
                    category.DataItems.Where(di => !modelCategory.DataItems.Select(mdi => mdi.Id).Contains(di.Id)).ToList();
                foreach (var di in deletedDataItems)
                {
                    category.DataItems.Remove(di);
                }

                foreach (var mdi in modelCategory.DataItems.Select((x, i) => new { Index = i, CategoryDataItem = x }).OrderBy(x => x.Index))
                {
                    var catDataItem = category.DataItems.Where(di => di.Id != 0 && di.Id == mdi.CategoryDataItem.Id).SingleOrDefault();
                    if (null == catDataItem)
                    {
                        catDataItem = new CategoryDataItem();
                    }
                    else
                    {
                        category.DataItems.Remove(catDataItem);
                    }
                    catDataItem.ShortName = string.Equals(mdi.CategoryDataItem.ShortName,
                                                          mdi.CategoryDataItem.DataItem.DataItemName)
                                                ? null
                                                : mdi.CategoryDataItem.ShortName;
                    if (mdi.Index < category.DataItems.Count)
                        category.DataItems.Insert(mdi.Index, catDataItem);
                    else
                        category.DataItems.Add(catDataItem);

                    if (null == catDataItem.DataItem || catDataItem.DataItem.Id != mdi.CategoryDataItem.DataItem.Id)
                    {
                        if (0 == mdi.CategoryDataItem.DataItem.Id)
                        {
                            var dataItems = DataItemRepo.Find("DataItemName", mdi.CategoryDataItem.DataItem.DataItemName);
                            catDataItem.DataItem = dataItems.Any() ? dataItems.First() : new DataItem { DataItemName = mdi.CategoryDataItem.DataItem.DataItemName };
                        }
                        else
                        {
                            catDataItem.DataItem = DataItemRepo.Get(mdi.CategoryDataItem.DataItem.Id);
                        }
                    }
                }
            }

            RebuildCategories(category.Subcategories, modelCategory.Subcategories);
        }

        private void SendAccountApprovedEmail(string name, string email)
        {
            string subject = ConfigurationManager.AppSettings["OrganisationName"] + " " + ConfigurationManager.AppSettings["CatalogueName"]+ " - Account Approved";
            var body = string.Format(
                "Dear {0},\n\nYour user account registration for the " + ConfigurationManager.AppSettings["OrganisationName"] + " " + ConfigurationManager.AppSettings["CatalogueName"] + " has now been approved.\n\n" +
                "You may now login at {1}Account/LogOn\n\n", name,
                Regex.Match(HttpContext.Request.Url.AbsoluteUri, @"^(http|https)://[a-zA-Z0-9\-\.]+(:[0-9]*)?/").Value);
            Mailer.Send(email, subject, body);
        }

        private void SendAccountUnlockedEmail(string name, string email)
        {
            string subject = ConfigurationManager.AppSettings["OrganisationName"] + " " + ConfigurationManager.AppSettings["CatalogueName"] + " - Account Unlocked";
            var body = string.Format(
                "Dear {0},\n\nYour user account for the "+ConfigurationManager.AppSettings["OrganisationName"] + " " + ConfigurationManager.AppSettings["CatalogueName"]+" has now been unlocked.\n\n" +
                "You may now login at {1}Account/LogOn\n\n", name,
                Regex.Match(HttpContext.Request.Url.AbsoluteUri, @"^(http|https)://[a-zA-Z0-9\-\.]+(:[0-9]*)?/").Value);
            Mailer.Send(email, subject, body);
        }

    }
}
