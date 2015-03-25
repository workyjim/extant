//-----------------------------------------------------------------------
// <copyright file="EntityExtensionsTests.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using Extant.Data.Entities;
using Extant.Web.Helpers;
using NUnit.Framework;

namespace Extant.Tests.Unit.Web.Helpers
{
    [TestFixture]
    public class EntityExtensionsTests
    {
        [Test]
        public void UpdateSamples_NumberOfDnaSamples()
        {
            var study = new Study {NumberOfDnaSamples = NumberOfSamples.Unknown, NumberOfDnaSamplesExact = null};
            var other = new Study { NumberOfDnaSamples = NumberOfSamples.N101To250, NumberOfDnaSamplesExact = 250 };
            study.UpdateSamples(other);
            Assert.AreEqual(other.NumberOfDnaSamples, study.NumberOfDnaSamples);
            Assert.AreEqual(other.NumberOfDnaSamplesExact, study.NumberOfDnaSamplesExact);
        }

        [Test]
        public void UpdateSamples_NumberOfSerumSamples()
        {
            var study = new Study { NumberOfSerumSamples = NumberOfSamples.Unknown, NumberOfSerumSamplesExact = null };
            var other = new Study { NumberOfSerumSamples = NumberOfSamples.N101To250, NumberOfSerumSamplesExact = 250 };
            study.UpdateSamples(other);
            Assert.AreEqual(other.NumberOfSerumSamples, study.NumberOfSerumSamples);
            Assert.AreEqual(other.NumberOfSerumSamplesExact, study.NumberOfSerumSamplesExact);
        }

        [Test]
        public void UpdateSamples_NumberOfPlasmaSamples()
        {
            var study = new Study { NumberOfPlasmaSamples = NumberOfSamples.Unknown, NumberOfPlasmaSamplesExact = null };
            var other = new Study { NumberOfPlasmaSamples = NumberOfSamples.N101To250, NumberOfPlasmaSamplesExact = 250 };
            study.UpdateSamples(other);
            Assert.AreEqual(other.NumberOfPlasmaSamples, study.NumberOfPlasmaSamples);
            Assert.AreEqual(other.NumberOfPlasmaSamplesExact, study.NumberOfPlasmaSamplesExact);
        }

        [Test]
        public void UpdateSamples_NumberOfWholeBloodSamples()
        {
            var study = new Study { NumberOfWholeBloodSamples = NumberOfSamples.Unknown, NumberOfWholeBloodSamplesExact = null };
            var other = new Study { NumberOfWholeBloodSamples = NumberOfSamples.N101To250, NumberOfWholeBloodSamplesExact = 250 };
            study.UpdateSamples(other);
            Assert.AreEqual(other.NumberOfWholeBloodSamples, study.NumberOfWholeBloodSamples);
            Assert.AreEqual(other.NumberOfWholeBloodSamplesExact, study.NumberOfWholeBloodSamplesExact);
        }

        [Test]
        public void UpdateSamples_NumberOfSalivaSamples()
        {
            var study = new Study { NumberOfSalivaSamples = NumberOfSamples.Unknown, NumberOfSalivaSamplesExact = null };
            var other = new Study { NumberOfSalivaSamples = NumberOfSamples.N101To250, NumberOfSalivaSamplesExact = 250 };
            study.UpdateSamples(other);
            Assert.AreEqual(other.NumberOfSalivaSamples, study.NumberOfSalivaSamples);
            Assert.AreEqual(other.NumberOfSalivaSamplesExact, study.NumberOfSalivaSamplesExact);
        }

        [Test]
        public void UpdateSamples_NumberOfTissueSamples()
        {
            var study = new Study { NumberOfTissueSamples = NumberOfSamples.Unknown, NumberOfTissueSamplesExact = null };
            var other = new Study { NumberOfTissueSamples = NumberOfSamples.N101To250, NumberOfTissueSamplesExact = 250 };
            study.UpdateSamples(other);
            Assert.AreEqual(other.NumberOfTissueSamples, study.NumberOfTissueSamples);
            Assert.AreEqual(other.NumberOfTissueSamplesExact, study.NumberOfTissueSamplesExact);
        }

        [Test]
        public void UpdateSamples_NumberOfCellSamples()
        {
            var study = new Study { NumberOfCellSamples = NumberOfSamples.Unknown, NumberOfCellSamplesExact = null };
            var other = new Study { NumberOfCellSamples = NumberOfSamples.N101To250, NumberOfCellSamplesExact = 250 };
            study.UpdateSamples(other);
            Assert.AreEqual(other.NumberOfCellSamples, study.NumberOfCellSamples);
            Assert.AreEqual(other.NumberOfCellSamplesExact, study.NumberOfCellSamplesExact);
        }

        [Test]
        public void UpdateSamples_NumberOfOtherSamples()
        {
            var study = new Study { NumberOfOtherSamples = NumberOfSamples.Unknown, NumberOfOtherSamplesExact = null };
            var other = new Study { NumberOfOtherSamples = NumberOfSamples.N101To250, NumberOfOtherSamplesExact = 250 };
            study.UpdateSamples(other);
            Assert.AreEqual(other.NumberOfOtherSamples, study.NumberOfOtherSamples);
            Assert.AreEqual(other.NumberOfOtherSamplesExact, study.NumberOfOtherSamplesExact);
        }

        [Test]
        public void UpdateSamples_DetailedSampleInfo()
        {
            var study = new Study { DetailedSampleInfo = false };
            var other = new Study { DetailedSampleInfo = true };
            study.UpdateSamples(other);
            Assert.AreEqual(other.DetailedSampleInfo, study.DetailedSampleInfo);
        }

        [Test]
        public void UpdateSamples_AddSample()
        {
            var study = new Study()
                            {
                                Samples = new List<Sample>()
                            };
            var other = new Study
                            {
                                Samples = new List<Sample>
                                              {
                                                  new Sample
                                                      {
                                                          SampleType = SampleType.DNA
                                                      }
                                              }
                            };
            study.UpdateSamples(other);
            Assert.AreEqual(1, study.Samples.Count);
            Assert.AreEqual(SampleType.DNA, study.Samples[0].SampleType);
        }

        [Test]
        public void UpdateSamples_UpdateSample()
        {
            var study = new Study()
            {
                Samples = new List<Sample>
                              {
                                  new Sample
                                      {
                                          SampleType = SampleType.DNA
                                      },
                                  new Sample
                                      {
                                          SampleType = SampleType.Serum
                                      }
                              }
            };
            var other = new Study
            {
                Samples = new List<Sample>
                              {
                                  new Sample
                                      {
                                          SampleType = SampleType.DNA
                                      },
                                  new Sample
                                      {
                                          SampleType = SampleType.Plasma
                                      }
                              }
            };
            study.UpdateSamples(other);
            Assert.AreEqual(2, study.Samples.Count);
            Assert.AreEqual(SampleType.DNA, study.Samples[0].SampleType);
            Assert.AreEqual(SampleType.Plasma, study.Samples[1].SampleType);

        }

        [Test]
        public void UpdateSamples_RemoveSample()
        {
            var study = new Study()
            {
                Samples = new List<Sample>
                              {
                                  new Sample
                                      {
                                          SampleType = SampleType.DNA
                                      },
                                  new Sample
                                      {
                                          SampleType = SampleType.Serum
                                      }
                              }
            };
            var other = new Study
            {
                Samples = new List<Sample>
                              {
                                  new Sample
                                      {
                                          SampleType = SampleType.Serum
                                      }
                              }
            };
            study.UpdateSamples(other);
            Assert.AreEqual(1, study.Samples.Count);
            Assert.AreEqual(SampleType.Serum, study.Samples[0].SampleType);

        }

        [Test]
        public void UpdateSample_SampleType()
        {
            var sample = new Sample {SampleType = SampleType.DNA};
            var other = new Sample {SampleType = SampleType.Serum};
            sample.UpdateSample(other);
            Assert.AreEqual(other.SampleType, sample.SampleType);
        }

        [Test]
        public void UpdateSample_SampleTypeSpecify()
        {
            var sample = new Sample { SampleTypeSpecify = "this" };
            var other = new Sample { SampleTypeSpecify = "that" };
            sample.UpdateSample(other);
            Assert.AreEqual(other.SampleTypeSpecify, sample.SampleTypeSpecify);
        }

        [Test]
        public void UpdateSample_BioMatWholeBlood()
        {
            var sample = new Sample { BioMatWholeBlood = false };
            var other = new Sample { BioMatWholeBlood = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatWholeBlood, sample.BioMatWholeBlood);
        }

        [Test]
        public void UpdateSample_BioMatBuffyCoat()
        {
            var sample = new Sample { BioMatBuffyCoat = false };
            var other = new Sample { BioMatBuffyCoat = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatBuffyCoat, sample.BioMatBuffyCoat);
        }

        [Test]
        public void UpdateSample_BioMatSaliva()
        {
            var sample = new Sample { BioMatSaliva = false };
            var other = new Sample { BioMatSaliva = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatSaliva, sample.BioMatSaliva);
        }

        [Test]
        public void UpdateSample_BioMatBuccalSwabs()
        {
            var sample = new Sample { BioMatBuccalSwabs = false };
            var other = new Sample { BioMatBuccalSwabs = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatBuccalSwabs, sample.BioMatBuccalSwabs);
        }

        [Test]
        public void UpdateSample_BioMatAcidCitrateDextrose()
        {
            var sample = new Sample { BioMatAcidCitrateDextrose = false };
            var other = new Sample { BioMatAcidCitrateDextrose = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatAcidCitrateDextrose, sample.BioMatAcidCitrateDextrose);
        }

        [Test]
        public void UpdateSample_BioMatSynovialFluid()
        {
            var sample = new Sample { BioMatSynovialFluid = false };
            var other = new Sample { BioMatSynovialFluid = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatSynovialFluid, sample.BioMatSynovialFluid);
        }

        [Test]
        public void UpdateSample_BioMatSynovialTissue()
        {
            var sample = new Sample { BioMatSynovialTissue = false };
            var other = new Sample { BioMatSynovialTissue = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatSynovialTissue, sample.BioMatSynovialTissue);
        }

        [Test]
        public void UpdateSample_BioMatSerumSeparatorTube()
        {
            var sample = new Sample { BioMatSerumSeparatorTube = false };
            var other = new Sample { BioMatSerumSeparatorTube = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatSerumSeparatorTube, sample.BioMatSerumSeparatorTube);
        }

        [Test]
        public void UpdateSample_BioMatPlasmaSeparatorTube()
        {
            var sample = new Sample { BioMatPlasmaSeparatorTube = false };
            var other = new Sample { BioMatPlasmaSeparatorTube = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatPlasmaSeparatorTube, sample.BioMatPlasmaSeparatorTube);
        }

        [Test]
        public void UpdateSample_BioMatUrine()
        {
            var sample = new Sample { BioMatUrine = false };
            var other = new Sample { BioMatUrine = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatUrine, sample.BioMatUrine);
        }

        [Test]
        public void UpdateSample_BioMatOtherTubes()
        {
            var sample = new Sample { BioMatOtherTubes = false };
            var other = new Sample { BioMatOtherTubes = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatOtherTubes, sample.BioMatOtherTubes);
        }

        [Test]
        public void UpdateSample_BioMatEdtaBlood()
        {
            var sample = new Sample { BioMatEdtaBlood = false };
            var other = new Sample { BioMatEdtaBlood = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatEdtaBlood, sample.BioMatEdtaBlood);
        }

        [Test]
        public void UpdateSample_BioMatSalivaNoAdditive()
        {
            var sample = new Sample { BioMatSalivaNoAdditive = false };
            var other = new Sample { BioMatSalivaNoAdditive = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatSalivaNoAdditive, sample.BioMatSalivaNoAdditive);
        }

        [Test]
        public void UpdateSample_BioMatSalivaOragene()
        {
            var sample = new Sample { BioMatSalivaOragene = false };
            var other = new Sample { BioMatSalivaOragene = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatSalivaOragene, sample.BioMatSalivaOragene);
        }

        [Test]
        public void UpdateSample_BioMatCulture()
        {
            var sample = new Sample { BioMatCulture = false };
            var other = new Sample { BioMatCulture = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatCulture, sample.BioMatCulture);
        }

        [Test]
        public void UpdateSample_BioMatUnknown()
        {
            var sample = new Sample { BioMatUnknown = false };
            var other = new Sample { BioMatUnknown = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatUnknown, sample.BioMatUnknown);
        }

        [Test]
        public void UpdateSample_BioMatOther()
        {
            var sample = new Sample { BioMatOther = false };
            var other = new Sample { BioMatOther = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatOther, sample.BioMatOther);
        }

        [Test]
        public void UpdateSample_BioMatOtherTubesSpecify()
        {
            var sample = new Sample { BioMatOtherTubesSpecify = "this" };
            var other = new Sample { BioMatOtherTubesSpecify = "that" };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatOtherTubesSpecify, sample.BioMatOtherTubesSpecify);
        }

        [Test]
        public void UpdateSample_BioMatCultureSpecify()
        {
            var sample = new Sample { BioMatCultureSpecify = "this" };
            var other = new Sample { BioMatCultureSpecify = "that" };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatCultureSpecify, sample.BioMatCultureSpecify);
        }

        [Test]
        public void UpdateSample_BioMatOtherSpecify()
        {
            var sample = new Sample { BioMatOtherSpecify = "this" };
            var other = new Sample { BioMatOtherSpecify = "that" };
            sample.UpdateSample(other);
            Assert.AreEqual(other.BioMatOtherSpecify, sample.BioMatOtherSpecify);
        }

        [Test]
        public void UpdateSample_TissueSamplesPreserved()
        {
            var sample = new Sample { TissueSamplesPreserved = TissueSamplesPreserved.Other };
            var other = new Sample { TissueSamplesPreserved = TissueSamplesPreserved.FormalinFixedParaffinEmbedded };
            sample.UpdateSample(other);
            Assert.AreEqual(other.TissueSamplesPreserved, sample.TissueSamplesPreserved);
        }

        [Test]
        public void UpdateSample_TissueSamplesPreservedSpecify()
        {
            var sample = new Sample { TissueSamplesPreservedSpecify = "this" };
            var other = new Sample { TissueSamplesPreservedSpecify = "that" };
            sample.UpdateSample(other);
            Assert.AreEqual(other.TissueSamplesPreservedSpecify, sample.TissueSamplesPreservedSpecify);
        }

        [Test]
        public void UpdateSample_SampleVolume()
        {
            var sample = new Sample { SampleVolume = SampleVolume.V5To10ml };
            var other = new Sample { SampleVolume = SampleVolume.V2000ulPlus };
            sample.UpdateSample(other);
            Assert.AreEqual(other.SampleVolume, sample.SampleVolume);
        }

        [Test]
        public void UpdateSample_SampleVolumeSpecify()
        {
            var sample = new Sample { SampleVolumeSpecify = "this" };
            var other = new Sample { SampleVolumeSpecify = "that" };
            sample.UpdateSample(other);
            Assert.AreEqual(other.SampleVolumeSpecify, sample.SampleVolumeSpecify);
        }

        [Test]
        public void UpdateSample_CellCount()
        {
            var sample = new Sample { CellCount = null };
            var other = new Sample() { CellCount = 24 };
            sample.UpdateSample(other);
            Assert.AreEqual(other.CellCount, sample.CellCount);
        }

        [Test]
        public void UpdateSample_Concentration()
        {
            var sample = new Sample { Concentration = Concentration.C200To300ngul };
            var other = new Sample { Concentration = Concentration.CGt300ngul };
            sample.UpdateSample(other);
            Assert.AreEqual(other.Concentration, sample.Concentration);
        }

        [Test]
        public void UpdateSample_ConcentrationSpecify()
        {
            var sample = new Sample { ConcentrationSpecify = "this" };
            var other = new Sample { ConcentrationSpecify = "that" };
            sample.UpdateSample(other);
            Assert.AreEqual(other.ConcentrationSpecify, sample.ConcentrationSpecify);
        }

        [Test]
        public void UpdateSample_NumberOfAliquots()
        {
            var sample = new Sample { NumberOfAliquots = NumberOfAliquots.Unknown };
            var other = new Sample { NumberOfAliquots = NumberOfAliquots.A5To10 };
            sample.UpdateSample(other);
            Assert.AreEqual(other.NumberOfAliquots, sample.NumberOfAliquots);
        }

        [Test]
        public void UpdateSample_WhenCollected()
        {
            var sample = new Sample { WhenCollected = WhenCollected.WGt10Years };
            var other = new Sample { WhenCollected = WhenCollected.W2To5Years };
            sample.UpdateSample(other);
            Assert.AreEqual(other.WhenCollected, sample.WhenCollected);
        }

        [Test]
        public void UpdateSample_SnapFrozen()
        {
            var sample = new Sample { SnapFrozen = YesNoUnknown.Unknown };
            var other = new Sample { SnapFrozen = YesNoUnknown.Yes };
            sample.UpdateSample(other);
            Assert.AreEqual(other.SnapFrozen, sample.SnapFrozen);
        }

        [Test]
        public void UpdateSample_HowDnaExtracted()
        {
            var sample = new Sample { HowDnaExtracted = HowDnaExtracted.Unknown };
            var other = new Sample { HowDnaExtracted = HowDnaExtracted.PhenolChloroform };
            sample.UpdateSample(other);
            Assert.AreEqual(other.HowDnaExtracted, sample.HowDnaExtracted);
        }

        [Test]
        public void UpdateSample_HowDnaExtractedSpecify()
        {
            var sample = new Sample { HowDnaExtractedSpecify = "this" };
            var other = new Sample { HowDnaExtractedSpecify = "that" };
            sample.UpdateSample(other);
            Assert.AreEqual(other.HowDnaExtractedSpecify, sample.HowDnaExtractedSpecify);
        }

        [Test]
        public void UpdateSample_TimeBetweenCollectionAndStorage()
        {
            var sample = new Sample { TimeBetweenCollectionAndStorage = TimeBetweenCollectionAndStorage.T5To24Hours };
            var other = new Sample { TimeBetweenCollectionAndStorage = TimeBetweenCollectionAndStorage.Unknown };
            sample.UpdateSample(other);
            Assert.AreEqual(other.TimeBetweenCollectionAndStorage, sample.TimeBetweenCollectionAndStorage);
        }

        [Test]
        public void UpdateSample_CollectionToStorageTemp()
        {
            var sample = new Sample { CollectionToStorageTemp = CollectionToStorageTemp.Unknown };
            var other = new Sample { CollectionToStorageTemp = CollectionToStorageTemp.Minus80C };
            sample.UpdateSample(other);
            Assert.AreEqual(other.CollectionToStorageTemp, sample.CollectionToStorageTemp);
        }

        [Test]
        public void UpdateSample_CollectionToStorageTempSpecify()
        {
            var sample = new Sample { CollectionToStorageTempSpecify = "this" };
            var other = new Sample { CollectionToStorageTempSpecify = "that" };
            sample.UpdateSample(other);
            Assert.AreEqual(other.CollectionToStorageTempSpecify, sample.CollectionToStorageTempSpecify);
        }

        [Test]
        public void UpdateSample_StorageTemp()
        {
            var sample = new Sample { StorageTemp = StorageTemp.Unknown };
            var other = new Sample { StorageTemp = StorageTemp.Minus80C };
            sample.UpdateSample(other);
            Assert.AreEqual(other.StorageTemp, sample.StorageTemp);
        }

        [Test]
        public void UpdateSample_StorageTempSpecify()
        {
            var sample = new Sample { StorageTempSpecify = "this" };
            var other = new Sample { StorageTempSpecify = "that" };
            sample.UpdateSample(other);
            Assert.AreEqual(other.StorageTempSpecify, sample.StorageTempSpecify);
        }

        [Test]
        public void UpdateSample_AlwayStoredAtThisTemp()
        {
            var sample = new Sample { AlwayStoredAtThisTemp = YesNoUnknown.Unknown };
            var other = new Sample { AlwayStoredAtThisTemp = YesNoUnknown.No };
            sample.UpdateSample(other);
            Assert.AreEqual(other.AlwayStoredAtThisTemp, sample.AlwayStoredAtThisTemp);
        }

        [Test]
        public void UpdateSample_DnaQualityAbsorbance()
        {
            var sample = new Sample { DnaQualityAbsorbance = false };
            var other = new Sample { DnaQualityAbsorbance = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.DnaQualityAbsorbance, sample.DnaQualityAbsorbance);
        }

        [Test]
        public void UpdateSample_DnaQualityGel()
        {
            var sample = new Sample { DnaQualityGel = false };
            var other = new Sample { DnaQualityGel = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.DnaQualityGel, sample.DnaQualityGel);
        }

        [Test]
        public void UpdateSample_DnaQualityCommercialKit()
        {
            var sample = new Sample { DnaQualityCommercialKit = false };
            var other = new Sample { DnaQualityCommercialKit = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.DnaQualityCommercialKit, sample.DnaQualityCommercialKit);
        }

        [Test]
        public void UpdateSample_DnaQualityPcr()
        {
            var sample = new Sample { DnaQualityPcr = false };
            var other = new Sample { DnaQualityPcr = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.DnaQualityPcr, sample.DnaQualityPcr);
        }

        [Test]
        public void UpdateSample_DnaQualityPicoGreen()
        {
            var sample = new Sample { DnaQualityPicoGreen = false };
            var other = new Sample { DnaQualityPicoGreen = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.DnaQualityPicoGreen, sample.DnaQualityPicoGreen);
        }

        [Test]
        public void UpdateSample_DnaQualityUnknown()
        {
            var sample = new Sample { DnaQualityUnknown = false };
            var other = new Sample { DnaQualityUnknown = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.DnaQualityUnknown, sample.DnaQualityUnknown);
        }

        [Test]
        public void UpdateSample_DnaQualityOther()
        {
            var sample = new Sample { DnaQualityOther = false };
            var other = new Sample { DnaQualityOther = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.DnaQualityOther, sample.DnaQualityOther);
        }

        [Test]
        public void UpdateSample_DnaQualityOtherSpecify()
        {
            var sample = new Sample { DnaQualityOtherSpecify = "this" };
            var other = new Sample { DnaQualityOtherSpecify = "that" };
            sample.UpdateSample(other);
            Assert.AreEqual(other.DnaQualityOtherSpecify, sample.DnaQualityOtherSpecify);
        }

        [Test]
        public void UpdateSample_FreezeThawCycles()
        {
            var sample = new Sample { FreezeThawCycles = FreezeThawCycles.Unknown };
            var other = new Sample { FreezeThawCycles = FreezeThawCycles.Three };
            sample.UpdateSample(other);
            Assert.AreEqual(other.FreezeThawCycles, sample.FreezeThawCycles);
        }

        [Test]
        public void UpdateSample_AnalysisNo()
        {
            var sample = new Sample { AnalysisNo = false };
            var other = new Sample { AnalysisNo = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.AnalysisNo, sample.AnalysisNo);
        }

        [Test]
        public void UpdateSample_AnalysisSequencing()
        {
            var sample = new Sample { AnalysisSequencing = false };
            var other = new Sample { AnalysisSequencing = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.AnalysisSequencing, sample.AnalysisSequencing);
        }

        [Test]
        public void UpdateSample_AnalysisRealTimePcr()
        {
            var sample = new Sample { AnalysisRealTimePcr = false };
            var other = new Sample { AnalysisRealTimePcr = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.AnalysisRealTimePcr, sample.AnalysisRealTimePcr);
        }

        [Test]
        public void UpdateSample_AnalysisPcr()
        {
            var sample = new Sample { AnalysisPcr = false };
            var other = new Sample { AnalysisPcr = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.AnalysisPcr, sample.AnalysisPcr);
        }

        [Test]
        public void UpdateSample_AnalysisGenotyping()
        {
            var sample = new Sample { AnalysisGenotyping = false };
            var other = new Sample { AnalysisGenotyping = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.AnalysisGenotyping, sample.AnalysisGenotyping);
        }

        [Test]
        public void UpdateSample_AnalysisBiochemistry()
        {
            var sample = new Sample { AnalysisBiochemistry = false };
            var other = new Sample { AnalysisBiochemistry = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.AnalysisBiochemistry, sample.AnalysisBiochemistry);
        }

        [Test]
        public void UpdateSample_AnalysisImmunochemistry()
        {
            var sample = new Sample { AnalysisImmunochemistry = false };
            var other = new Sample { AnalysisImmunochemistry = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.AnalysisImmunochemistry, sample.AnalysisImmunochemistry);
        }

        [Test]
        public void UpdateSample_AnalysisDnaExtraction()
        {
            var sample = new Sample { AnalysisDnaExtraction = false };
            var other = new Sample { AnalysisDnaExtraction = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.AnalysisDnaExtraction, sample.AnalysisDnaExtraction);
        }

        [Test]
        public void UpdateSample_AnalysisImmunohistochemistry()
        {
            var sample = new Sample { AnalysisImmunohistochemistry = false };
            var other = new Sample { AnalysisImmunohistochemistry = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.AnalysisImmunohistochemistry, sample.AnalysisImmunohistochemistry);
        }

        [Test]
        public void UpdateSample_AnalysisCellLinesDerived()
        {
            var sample = new Sample { AnalysisCellLinesDerived = false };
            var other = new Sample { AnalysisCellLinesDerived = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.AnalysisCellLinesDerived, sample.AnalysisCellLinesDerived);
        }

        [Test]
        public void UpdateSample_AnalysisRnaExtraction()
        {
            var sample = new Sample { AnalysisRnaExtraction = false };
            var other = new Sample { AnalysisRnaExtraction = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.AnalysisRnaExtraction, sample.AnalysisRnaExtraction);
        }

        [Test]
        public void UpdateSample_AnalysisUnknown()
        {
            var sample = new Sample { AnalysisUnknown = false };
            var other = new Sample { AnalysisUnknown = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.AnalysisUnknown, sample.AnalysisUnknown);
        }

        [Test]
        public void UpdateSample_AnalysisOther()
        {
            var sample = new Sample { AnalysisOther = false };
            var other = new Sample { AnalysisOther = true };
            sample.UpdateSample(other);
            Assert.AreEqual(other.AnalysisOther, sample.AnalysisOther);
        }

        [Test]
        public void UpdateSample_AnalysisOtherSpecify()
        {
            var sample = new Sample { AnalysisOtherSpecify = "this" };
            var other = new Sample { AnalysisOtherSpecify = "that" };
            sample.UpdateSample(other);
            Assert.AreEqual(other.AnalysisOtherSpecify, sample.AnalysisOtherSpecify);
        }

        [Test]
        public void UpdateSample_DnaExtracted()
        {
            var sample = new Sample { DnaExtracted = YesNoUnknown.Unknown };
            var other = new Sample { DnaExtracted = YesNoUnknown.Yes };
            sample.UpdateSample(other);
            Assert.AreEqual(other.DnaExtracted, sample.DnaExtracted);
        }

        [Test]
        public void UpdateSample_CellsGrown()
        {
            var sample = new Sample { CellsGrown = YesNoUnknown.Unknown };
            var other = new Sample { CellsGrown = YesNoUnknown.Yes };
            sample.UpdateSample(other);
            Assert.AreEqual(other.CellsGrown, sample.CellsGrown);
        }


    }
}