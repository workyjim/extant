//-----------------------------------------------------------------------
// <copyright file="SearchBoxModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Extant.Web.Models
{
    public class SearchBoxModel
    {
        public string Term { get; set; }

        public int? DiseaseArea { get; set; }

        public int? StudyDesign { get; set; }

        public int? StudyStatus { get; set; }

        public string Samples { get; set; }

        public IEnumerable<DiseaseAreaBasicModel> DiseaseAreas { get; set; }

        public bool FiltersUsed
        {
            get { return DiseaseArea.HasValue || StudyDesign.HasValue || StudyStatus.HasValue || !string.IsNullOrEmpty(Samples); }
        }
    }
}