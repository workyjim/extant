//-----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Extant.Data.Entities;
using Extant.Data.Repositories;
using Extant.Web.Models;

namespace Extant.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IStudyRepository StudyRepo;

        public HomeController(IStudyRepository studyRepo)
        {
            StudyRepo = studyRepo;
        }

        public ActionResult Index()
        {
            var studies = StudyRepo.GetLatestStudies(5);
            return View(Mapper.Map<IEnumerable<Study>, IEnumerable<StudyBasicModel>>(studies));
        }

    }
}
