//-----------------------------------------------------------------------
// <copyright file="DiseaseAreaModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Extant.Web.Models
{
    public class DiseaseAreaModel
    {
        public int Id { get; set; }

        public string DiseaseAreaName { get; set; }

        public string DiseaseAreaSynonyms { get; set; }

        public bool Published { get; set; }

        public IEnumerable<CategoryModel> Categories { get; set; }

    }
}