//-----------------------------------------------------------------------
// <copyright file="StudySamplesModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Extant.Web.Models
{
    public class StudySamplesModel
    {
        public int Id { get; set; }

        public string StudyName { get; set; }

        public int NumberOfDnaSamples { get; set; }

        public int? NumberOfDnaSamplesExact { get; set; }

        public int NumberOfSerumSamples { get; set; }

        public int? NumberOfSerumSamplesExact { get; set; }

        public int NumberOfPlasmaSamples { get; set; }

        public int? NumberOfPlasmaSamplesExact { get; set; }

        public int NumberOfWholeBloodSamples { get; set; }

        public int? NumberOfWholeBloodSamplesExact { get; set; }

        public int NumberOfSalivaSamples { get; set; }

        public int? NumberOfSalivaSamplesExact { get; set; }

        public int NumberOfTissueSamples { get; set; }

        public int? NumberOfTissueSamplesExact { get; set; }

        public int NumberOfCellSamples { get; set; }

        public int? NumberOfCellSamplesExact { get; set; }

        public int NumberOfOtherSamples { get; set; }

        public int? NumberOfOtherSamplesExact { get; set; }

        public bool DetailedSampleInfo { get; set; }

        public IEnumerable<SampleModel> Samples { get; set; }

        public bool IsNew { get; set; }
    }
}