//-----------------------------------------------------------------------
// <copyright file="SampleModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Extant.Web.Models
{
    public class SampleModel
    {
        public int Id { get; set; }

        public int SampleType { get; set; }

        public string SampleTypeSpecify { get; set; }

        public bool BioMatWholeBlood { get; set; }

        public bool BioMatBuffyCoat { get; set; }

        public bool BioMatSaliva { get; set; }

        public bool BioMatBuccalSwabs { get; set; }

        public bool BioMatAcidCitrateDextrose { get; set; }

        public bool BioMatSynovialFluid { get; set; }

        public bool BioMatSynovialTissue { get; set; }

        public bool BioMatSerumSeparatorTube { get; set; }

        public bool BioMatPlasmaSeparatorTube { get; set; }

        public bool BioMatUrine { get; set; }

        public bool BioMatOtherTubes { get; set; }

        public string BioMatOtherTubesSpecify { get; set; }

        public bool BioMatEdtaBlood { get; set; }

        public bool BioMatSalivaNoAdditive { get; set; }

        public bool BioMatSalivaOragene { get; set; }

        public bool BioMatCulture { get; set; }

        public string BioMatCultureSpecify { get; set; }

        public bool BioMatUnknown { get; set; }

        public bool BioMatOther { get; set; }

        public string BioMatOtherSpecify { get; set; }

        public int? TissueSamplesPreserved { get; set; }

        public string TissueSamplesPreservedSpecify { get; set; }

        public int NumberOfSamples { get; set; }

        public int? NumberOfSamplesExact { get; set; }

        public int SampleVolume { get; set; }

        public string SampleVolumeSpecify { get; set; }

        public int? CellCount { get; set; }

        public int? Concentration { get; set; }

        public string ConcentrationSpecify { get; set; }

        public int? NumberOfAliquots { get; set; }

        public int WhenCollected { get; set; }

        public int? SnapFrozen { get; set; }

        public int? HowDnaExtracted { get; set; }

        public string HowDnaExtractedSpecify { get; set; }

        public int TimeBetweenCollectionAndStorage { get; set; }

        public int CollectionToStorageTemp { get; set; }

        public string CollectionToStorageTempSpecify { get; set; }

        public int StorageTemp { get; set; }

        public string StorageTempSpecify { get; set; }

        public int AlwayStoredAtThisTemp { get; set; }

        public bool DnaQualityAbsorbance { get; set; }

        public bool DnaQualityGel { get; set; }

        public bool DnaQualityCommercialKit { get; set; }

        public bool DnaQualityPcr { get; set; }

        public bool DnaQualityPicoGreen { get; set; }

        public bool DnaQualityUnknown { get; set; }

        public bool DnaQualityOther { get; set; }

        public string DnaQualityOtherSpecify { get; set; }

        public int FreezeThawCycles { get; set; }

        public bool AnalysisNo { get; set; }

        public bool AnalysisSequencing { get; set; }

        public bool AnalysisRealTimePcr { get; set; }

        public bool AnalysisPcr { get; set; }

        public bool AnalysisGenotyping { get; set; }

        public bool AnalysisBiochemistry { get; set; }

        public bool AnalysisImmunochemistry { get; set; }

        public bool AnalysisDnaExtraction { get; set; }

        public bool AnalysisImmunohistochemistry { get; set; }

        public bool AnalysisCellLinesDerived { get; set; }

        public bool AnalysisRnaExtraction { get; set; }

        public bool AnalysisUnknown { get; set; }

        public bool AnalysisOther { get; set; }

        public string AnalysisOtherSpecify { get; set; }

        public int? DnaExtracted { get; set; }

        public int? CellsGrown { get; set; }
    }
}