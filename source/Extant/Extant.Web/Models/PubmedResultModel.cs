//-----------------------------------------------------------------------
// <copyright file="PubmedResultModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Extant.Pubmed;
using NWeH.Paging;

namespace Extant.Web.Models
{
    public class PubmedResultModel
    {
        public bool PubmedIdSearch { get; set; }

        public string SearchTerm { get; set; }

        public IPagedList<PubmedResult> SearchResults { get; set; }
    }
}