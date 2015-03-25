//-----------------------------------------------------------------------
// <copyright file="PubmedController.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Extant.Pubmed;
using Extant.Web.Models;
using NWeH.Paging;

namespace Extant.Web.Controllers
{
    public class PubmedController : Controller
    {
        private readonly IPubmedService PubmedService;

        private const int DefaultPageSize = 5;

        public PubmedController(IPubmedService pubmedService)
        {
            PubmedService = pubmedService;
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Get(string pmid)
        {
            var result = PubmedService.Summary(pmid);
            var resultAsList = null == result ? new List<PubmedResult>() : new List<PubmedResult> {result};
            return PartialView("PubmedResult", new PubmedResultModel
                                                   {
                                                       PubmedIdSearch = true,
                                                       SearchResults = resultAsList.ToPagedList(1, DefaultPageSize, resultAsList.Count),
                                                       SearchTerm = pmid
                                                   });
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Search(string term, int? page, int? pagesize)
        {
            int count;
            var result = PubmedService.Search(term, (page ?? 1) - 1, pagesize ?? DefaultPageSize, out count);
            return PartialView("PubmedResult", new PubmedResultModel
                                                   {
                                                       PubmedIdSearch = false,
                                                       SearchResults = result.ToPagedList(page ?? 1, pagesize ?? DefaultPageSize, count),
                                                       SearchTerm = term
                                                   });
        }

    }
}
