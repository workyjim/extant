//-----------------------------------------------------------------------
// <copyright file="SearchAdvancedResultsModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using NWeH.Paging;

namespace Extant.Web.Models
{
    public class SearchAdvancedResultsModel
    {
        public string Query { get; set; }
        public IPagedList<StudyBasicModel> Studies { get; set; }
    }
}