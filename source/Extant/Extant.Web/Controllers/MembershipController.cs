using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Extant.Web.Controllers
{
    public abstract class MembershipController : Controller
    {
        protected void ErrorCodeToModelError(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    ModelState.AddModelError("Email", "An account for that e-mail address already exists. Please enter a different e-mail address.");
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    ModelState.AddModelError("Password", "The password provided is invalid. Please enter a valid password value.");
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    ModelState.AddModelError("Email", "The email address provided is invalid. Please check the value and try again.");
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    ModelState.AddModelError("_FORM", "The password retrieval answer provided is invalid. Please check the value and try again.");
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    ModelState.AddModelError("_FORM", "The password retrieval question provided is invalid. Please check the value and try again.");
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    ModelState.AddModelError("UserName", "The user name provided is invalid. Please check the value and try again.");
                    break;
                case MembershipCreateStatus.ProviderError:
                    ModelState.AddModelError("_FORM", "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.");
                    break;
                case MembershipCreateStatus.UserRejected:
                    ModelState.AddModelError("_FORM", "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.");
                    break;
                default:
                    ModelState.AddModelError("_FORM", "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.");
                    break;
            }
        }
    }
}
