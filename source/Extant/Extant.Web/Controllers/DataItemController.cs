//-----------------------------------------------------------------------
// <copyright file="DataItemController.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Extant.Data.Entities;
using Extant.Data.Repositories;
using Extant.Web.Models;

namespace Extant.Web.Controllers
{
    public class DataItemController : Controller
    {
        private readonly IDataItemRepository DataItemRepo;

        public DataItemController(IDataItemRepository dataItemRepo)
        {
            DataItemRepo = dataItemRepo;
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Find(string term)
        {
            var dataitems = DataItemRepo.Search(term);
            return Json(dataitems.ToDictionary(di => di.Id, di => di.DataItemName).ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}