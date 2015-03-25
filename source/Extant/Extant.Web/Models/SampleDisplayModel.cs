//-----------------------------------------------------------------------
// <copyright file="SampleDisplayModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Extant.Web.Models
{
    public class SampleDisplayModel
    {
        public int Id { get; set; }

        public string SampleType { get; set; }

        public string SampleTypeSpecify { get; set; }

        public IEnumerable<string> SourceBiologicalMaterial { get; set; }

        public string TissueSamplesPreserved { get; set; }

        public string TissueSamplesPreservedSpecify { get; set; }

        public string NumberOfSamples { get; set; }

        public int? NumberOfSamplesExact { get; set; }

        public string SampleVolume { get; set; }

        public string SampleVolumeSpecify { get; set; }

        public int? CellCount { get; set; }

        public string Concentration { get; set; }

        public string ConcentrationSpecify { get; set; }

        public string NumberOfAliquots { get; set; }

        public string WhenCollected { get; set; }

        public string SnapFrozen { get; set; }

        public string HowDnaExtracted { get; set; }

        public string HowDnaExtractedSpecify { get; set; }

        public string TimeBetweenCollectionAndStorage { get; set; }

        public string CollectionToStorageTemp { get; set; }

        public string CollectionToStorageTempSpecify { get; set; }

        public string StorageTemp { get; set; }

        public string StorageTempSpecify { get; set; }

        public string AlwayStoredAtThisTemp { get; set; }

        public IEnumerable<string> DnaQuality { get; set; }

        public string FreezeThawCycles { get; set; }

        public IEnumerable<string> Analysis { get; set; }

        public string DnaExtracted { get; set; }

        public string CellsGrown { get; set; }
    }
}