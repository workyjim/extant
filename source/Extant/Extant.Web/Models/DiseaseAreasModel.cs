//-----------------------------------------------------------------------
// <copyright file="DiseaseAreasModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Extant.Web.Models
{
    public class DiseaseAreasModel
    {
        public bool IsAdmin { get; set; }
        public IEnumerable<DiseaseAreaBasicModel> DiseaseAreas { get; set; }
    }
}