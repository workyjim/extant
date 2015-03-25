//-----------------------------------------------------------------------
// <copyright file="SearchResultsModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using NWeH.Paging;

namespace Extant.Web.Models
{
    public class SearchResultsModel
    {
        public string Term { get; set; }

        public int? DiseaseArea { get; set; }

        public int? StudyDesign { get; set; }

        public int? StudyStatus { get; set; }

        public string Samples { get; set; }

        public IEnumerable<DiseaseAreaBasicModel> DiseaseAreas { get; set; }

        public IPagedList<StudyBasicModel> Studies { get; set; }
    }
}