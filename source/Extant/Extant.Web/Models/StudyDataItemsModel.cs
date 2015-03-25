//-----------------------------------------------------------------------
// <copyright file="StudyDataItemsNewModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Extant.Web.Models
{
    public class StudyDataItemsModel
    {
        public int Id { get; set; }

        public string StudyName { get; set; }

        public bool IsLongitudinal { get; set; }

        public bool UseTimePoints { get; set; }

        public IEnumerable<DiseaseAreaModel> DiseaseAreas { get; set; }

        public IEnumerable<StudyDataItemModel> DataItems { get; set; }

        public IEnumerable<TimePointModel> TimePoints { get; set; }

        public bool IsNew { get; set; }
    }
}