//-----------------------------------------------------------------------
// <copyright file="Sample.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.ComponentModel;

namespace Extant.Data.Entities
{
    public class Sample : Entity
    {
        public virtual SampleType SampleType { get; set; }

        public virtual string SampleTypeSpecify { get; set; }

        public virtual NumberOfSamples NumberOfSamples { get; set; }

        public virtual int? NumberOfSamplesExact { get; set; }

        #region Please specify the source biological material from which the samples were isolated.

        public virtual bool BioMatWholeBlood { get; set; }

        public virtual bool BioMatBuffyCoat { get; set; }

        public virtual bool BioMatSaliva { get; set; }

        public virtual bool BioMatBuccalSwabs { get; set; }

        public virtual bool BioMatAcidCitrateDextrose { get; set; }

        public virtual bool BioMatSynovialFluid { get; set; }

        public virtual bool BioMatSynovialTissue { get; set; }

        public virtual bool BioMatSerumSeparatorTube { get; set; }

        public virtual bool BioMatPlasmaSeparatorTube { get; set; }

        public virtual bool BioMatUrine { get; set; }

        public virtual bool BioMatOtherTubes { get; set; }

        public virtual bool BioMatEdtaBlood { get; set; }

        public virtual bool BioMatSalivaNoAdditive { get; set; }

        public virtual bool BioMatSalivaOragene { get; set; }

        public virtual bool BioMatCulture { get; set; }

        public virtual bool BioMatUnknown { get; set; }

        public virtual bool BioMatOther { get; set; }

        public virtual string BioMatOtherTubesSpecify { get; set; }

        public virtual string BioMatCultureSpecify { get; set; }

        public virtual string BioMatOtherSpecify { get; set; }

        #endregion

        public virtual TissueSamplesPreserved? TissueSamplesPreserved { get; set; }

        public virtual string TissueSamplesPreservedSpecify { get; set; }

        public virtual SampleVolume SampleVolume { get; set; }

        public virtual string SampleVolumeSpecify { get; set; }

        public virtual int? CellCount { get; set; }

        public virtual Concentration? Concentration { get; set; }

        public virtual string ConcentrationSpecify { get; set; }

        public virtual NumberOfAliquots? NumberOfAliquots { get; set; }

        public virtual WhenCollected WhenCollected { get; set; }

        public virtual YesNoUnknown? SnapFrozen { get; set; }

        public virtual HowDnaExtracted? HowDnaExtracted { get; set; }

        public virtual string HowDnaExtractedSpecify { get; set; }

        public virtual TimeBetweenCollectionAndStorage? TimeBetweenCollectionAndStorage { get; set; }

        public virtual CollectionToStorageTemp CollectionToStorageTemp { get; set; }

        public virtual string CollectionToStorageTempSpecify { get; set; }

        public virtual StorageTemp StorageTemp { get; set; }

        public virtual string StorageTempSpecify { get; set; }

        public virtual YesNoUnknown AlwayStoredAtThisTemp { get; set; }

        #region Has the DNA quality been assessed by any of the following?

        public virtual bool DnaQualityAbsorbance { get; set; }

        public virtual bool DnaQualityGel { get; set; }

        public virtual bool DnaQualityCommercialKit { get; set; }

        public virtual bool DnaQualityPcr { get; set; }

        public virtual bool DnaQualityPicoGreen { get; set; }

        public virtual bool DnaQualityUnknown { get; set; }

        public virtual bool DnaQualityOther { get; set; }

        public virtual string DnaQualityOtherSpecify { get; set; }

        #endregion

        public virtual FreezeThawCycles FreezeThawCycles { get; set; }

        #region Has successful analysis been performed on the sample since collection?

        public virtual bool AnalysisNo { get; set; }

        public virtual bool AnalysisSequencing { get; set; }

        public virtual bool AnalysisRealTimePcr { get; set; }

        public virtual bool AnalysisPcr { get; set; }

        public virtual bool AnalysisGenotyping { get; set; }

        public virtual bool AnalysisBiochemistry { get; set; }

        public virtual bool AnalysisImmunochemistry { get; set; }

        public virtual bool AnalysisDnaExtraction { get; set; }

        public virtual bool AnalysisImmunohistochemistry { get; set; }

        public virtual bool AnalysisCellLinesDerived { get; set; }

        public virtual bool AnalysisRnaExtraction { get; set; }

        public virtual bool AnalysisUnknown { get; set; }

        public virtual bool AnalysisOther { get; set; }

        public virtual string AnalysisOtherSpecify { get; set; }

        #endregion

        public virtual YesNoUnknown? DnaExtracted { get; set; }

        public virtual YesNoUnknown? CellsGrown { get; set; }

    }

    public enum SampleType
    {
        DNA = 1,
        Serum = 2,
        Plasma = 3,
        WholeBlood = 4,
        Saliva = 5,
        Tissue = 6,
        Cell = 7,
        [Description("Other (please specify)")]
        Other = 8
    }

    public enum TissueSamplesPreserved
    {
        Unknown = 0,
        FormalinFixedParaffinEmbedded = 1,
        [Description("Other (please specify)")]
        Other = 3
    }

    public enum SampleVolume
    {
        Unknown = 0,
        [Description("0-50 &#956;l")]
        V0To50ul = 1,
        [Description("50-100 &#956;l")]
        V50To100ul = 2,
        [Description("100-200 &#956;l")]
        V100To200ul = 3,
        [Description("200-500 &#956;l")]
        V200To500ul = 4,
        [Description("500-750 &#956;l")]
        V500To750ul = 5,
        [Description("750-1000 &#956;l")]
        V750To1000ul = 6,
        [Description("1000-2000 &#956;l")]
        V1000To2000ul = 7,
        [Description("&gt;2000 &#956;l")]
        V2000ulPlus = 8,
        [Description("&lt;1 ml")]
        VLt1ml = 9,
        [Description("1-5 ml")]
        V1To5ml = 10,
        [Description("5-10 ml")]
        V5To10ml = 11,
        [Description("&gt;10 ml")]
        V10mlPlus = 12,
        [Description("Mixed volumes (please specify)")]
        MixedVolumes = 13
    }

    public enum Concentration
    {
        Unknown = 0,
        [Description("0-50 ng/&#956;l")]
        C0To50ngul = 1,
        [Description("50-100 ng/&#956;l")]
        C50To100ngul = 2,
        [Description("100-200 ng/&#956;l")]
        C100To200ngul = 3,
        [Description("200-300 ng/&#956;l")]
        C200To300ngul = 4,
        [Description("&gt;300 ng/&#956;l")]
        CGt300ngul = 5,
        [Description("Mixed Concentrations (please specify)")]
        MixedConcentrations = 6
    }

    public enum NumberOfAliquots
    {
        Unknown = 0,
        [Description("&lt;2")]
        ALt2 = 1,
        [Description("2-5")]
        A2To5 = 2,
        [Description("5-10")]
        A5To10 = 3,
        [Description("&gt;10")]
        AGt10 = 4
    }

    public enum WhenCollected
    {
        Unknown = 0,
        [Description("0-6 Months")]
        W0To6Months = 1,
        [Description("6-12 Months")]
        W6tO12Months = 2,
        [Description("1-2 Years")]
        W1To2Years = 3,
        [Description("2-5 Years")]
        W2To5Years = 4,
        [Description("5-10 Years")]
        W5To10Years = 5,
        [Description("&gt;10 Years")]
        WGt10Years = 6
    }

    public enum TimeBetweenCollectionAndStorage
    {
        Unknown = 0,
        [Description("0-4 Hours")]
        T0To4Hours = 1,
        [Description("5-24 Hours")]
        T5To24Hours = 2,
        [Description("24-36 Hours")]
        T24To36Hours = 3,
        [Description("&gt;36 Hours")]
        TGt36Hours = 4
    }

    public enum YesNoUnknown
    {
        Unknown = 0,
        Yes = 1,
        No = 2
    }

    public enum CollectionToStorageTemp
    {
        Unknown = 0,
        RoomTemperature = 1,
        [Description("+18 &deg;C")]
        Plus18C = 2,
        [Description("+4 &deg;C")]
        Plus4C = 3,
        [Description("-20 &deg;C")]
        Minus20C = 4,
        [Description("-80 &deg;C")]
        Minus80C = 5,
        LiquidNitrogen = 6,
        [Description("Other (please specify)")]
        Other = 7
    }

    public enum StorageTemp
    {
        Unknown = 0,
        RoomTemperature = 1,
        [Description("+4 &deg;C")]
        Plus4C = 2,
        [Description("-20 &deg;C")]
        Minus20C = 3,
        [Description("-80 &deg;C")]
        Minus80C = 4,
        LiquidNitrogen = 5,
        [Description("Other (please specify)")]
        Other = 6
    }

    public enum HowDnaExtracted
    {
        Unknown = 0,
        PhenolChloroform = 1,
        OtherPrecipitationMethod = 2,
        MagneticBeadChemistry = 3,
        [Description("Other (please specify)")]
        Other = 4
    }

    public enum FreezeThawCycles
    {
        [Description("0")]
        Zero = 0,
        [Description("1")]
        One = 1,
        [Description("2")]
        Two = 2,
        [Description("3")]
        Three = 3,
        [Description("4")]
        Four = 4,
        [Description("5")]
        Five = 5,
        [Description("6")]
        Six = 6,
        [Description("7")]
        Seven = 7,
        [Description("8")]
        Eight = 8,
        [Description("9")]
        Nine = 9,
        [Description("10+")]
        TenPlus = 10,
        Unknown = 11
    }
}