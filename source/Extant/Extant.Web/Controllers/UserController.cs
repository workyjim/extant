using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Extant.Data.Repositories;
using Extant.Web.Infrastructure;

namespace Extant.Web.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserRepository UserRepo;

        public UserController(IUserRepository userRepo)
        {
            UserRepo = userRepo;
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [AjaxAuthorize]
        public ActionResult Find(string term)
        {
            var users = UserRepo.FindUsers(term);
            return Json(users.ToDictionary(di => di.Id, di => di.UserName).ToArray(), JsonRequestBehavior.AllowGet);
        }

    }
}
