//-----------------------------------------------------------------------
// <copyright file="EntityExtensions.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using Extant.Data.Entities;
using Extant.Data.Repositories;
using Extant.Web.Models;

namespace Extant.Web.Helpers
{
    public static class EntityExtensions
    {
        public static IEnumerable<FileUpload> UpdateDetails(this Study study, Study other, bool removePatientInfo, 
            bool removeConsent, bool removeDataAccess, IEnumerable<int> additionalFilesToRemove)
        {
            var toDelete = new List<FileUpload>();
            study.StudyName = other.StudyName;
            study.Description = other.Description;
            study.StudySynonyms = other.StudySynonyms;
            study.StudyWebsite = other.StudyWebsite;
            study.DiseaseAreas = other.DiseaseAreas;
            study.StudyDesign = other.StudyDesign;
            study.StartDate = other.StartDate;
            study.StudyStatus = other.StudyStatus;
            study.RecruitmentTarget = other.RecruitmentTarget;
            study.ParticipantsRecruited = other.ParticipantsRecruited;
            study.PrincipalInvestigator = other.PrincipalInvestigator;
            study.Institution = other.Institution;
            study.Funder = other.Funder;
            study.OnPortfolio = other.OnPortfolio;
            study.PortfolioNumber = other.PortfolioNumber;
            study.ContactName = other.ContactName;
            study.ContactAddress = other.ContactAddress;
            study.ContactPhone = other.ContactPhone;
            study.ContactEmail = other.ContactEmail;
            study.HasDataAccessPolicy = other.HasDataAccessPolicy;
            study.IsLongitudinal = other.IsLongitudinal;

            var deletedTimePoints =
                study.TimePoints.Where(tp => !other.TimePoints.Select(otp => otp.Id).Contains(tp.Id)).ToList();
            foreach ( var tp in deletedTimePoints)
            {
                study.TimePoints.Remove(tp);
            }
            foreach ( var tp in study.TimePoints)
            {
                var otp = other.TimePoints.Single(x => x.Id == tp.Id);
                tp.Name = otp.Name;
            }
            foreach ( var tp in other.TimePoints.Where(otp => 0 == otp.Id))
            {
                study.TimePoints.Add(tp);
            }

            if (removePatientInfo)
            {
                toDelete.Add(study.PatientInformationLeaflet);
                study.PatientInformationLeaflet = null;
            }
            if (null != other.PatientInformationLeaflet)
            {
                if ( !removePatientInfo && null != study.PatientInformationLeaflet ) toDelete.Add(study.PatientInformationLeaflet);
                study.PatientInformationLeaflet = other.PatientInformationLeaflet;
            }

            if (removeConsent)
            {
                toDelete.Add(study.ConsentForm);
                study.ConsentForm = null;
            }
            if (null != other.ConsentForm)
            {
                if (!removeConsent && null != study.ConsentForm) toDelete.Add(study.ConsentForm);
                study.ConsentForm = other.ConsentForm;
            }

            if (removeDataAccess)
            {
                toDelete.Add(study.DataAccessPolicy);
                study.DataAccessPolicy = null;
            }
            if (null != other.DataAccessPolicy)
            {
                if (!removeDataAccess && null != study.DataAccessPolicy) toDelete.Add(study.DataAccessPolicy);
                study.DataAccessPolicy = other.DataAccessPolicy;
            }

            var deletedFiles = new List<AdditionalDocument>();
            foreach ( var file in study.AdditionalDocuments )
            {
                if (additionalFilesToRemove.Contains(file.Id))
                {
                    deletedFiles.Add(file);
                }
                else
                {
                    var otherFile = other.AdditionalDocuments.Where(ad => ad.Id == file.Id).Single();
                    file.Description = otherFile.Description;
                    file.DocumentType = otherFile.DocumentType;
                    if ( null != otherFile.File)
                    {
                        toDelete.Add(file.File);
                        file.File = otherFile.File;
                    }
                }
            }

            foreach (var deleted in deletedFiles )
            {
                study.AdditionalDocuments.Remove(deleted);
                toDelete.Add(deleted.File);
            }

            foreach ( var newFile in other.AdditionalDocuments.Where(ad => 0 == ad.Id))
            {
                study.AdditionalDocuments.Add(newFile);
            }

            return toDelete;
        }

        public static IEnumerable<Sample> UpdateSamples(this Study study, Study other)
        {
            var toDelete = new List<Sample>();
            study.NumberOfDnaSamples = other.NumberOfDnaSamples;
            study.NumberOfDnaSamplesExact = other.NumberOfDnaSamplesExact;
            study.NumberOfSerumSamples = other.NumberOfSerumSamples;
            study.NumberOfSerumSamplesExact = other.NumberOfSerumSamplesExact;
            study.NumberOfPlasmaSamples = other.NumberOfPlasmaSamples;
            study.NumberOfPlasmaSamplesExact = other.NumberOfPlasmaSamplesExact;
            study.NumberOfWholeBloodSamples = other.NumberOfWholeBloodSamples;
            study.NumberOfWholeBloodSamplesExact = other.NumberOfWholeBloodSamplesExact;
            study.NumberOfSalivaSamples = other.NumberOfSalivaSamples;
            study.NumberOfSalivaSamplesExact = other.NumberOfSalivaSamplesExact;
            study.NumberOfTissueSamples = other.NumberOfTissueSamples;
            study.NumberOfTissueSamplesExact = other.NumberOfTissueSamplesExact;
            study.NumberOfCellSamples = other.NumberOfCellSamples;
            study.NumberOfCellSamplesExact = other.NumberOfCellSamplesExact;
            study.NumberOfOtherSamples = other.NumberOfOtherSamples;
            study.NumberOfOtherSamplesExact = other.NumberOfOtherSamplesExact;
            study.DetailedSampleInfo = other.DetailedSampleInfo;

            for (int i = study.Samples.Count() - 1; i >= other.Samples.Count(); i--)
            {
                study.Samples.RemoveAt(i);
            }

            foreach (var sample in other.Samples.Select((x, i) => new { Index = i, Sample = x }))
            {
                if (sample.Index < study.Samples.Count())
                {
                    study.Samples[sample.Index].UpdateSample(sample.Sample);
                }
                else
                {
                    study.Samples.Add(sample.Sample);
                }
            }

            return toDelete;
        }

        public static void UpdateSample(this Sample sample, Sample other)
        {
            sample.SampleType = other.SampleType;
            sample.SampleTypeSpecify = other.SampleTypeSpecify;
            sample.BioMatWholeBlood = other.BioMatWholeBlood;
            sample.BioMatBuffyCoat = other.BioMatBuffyCoat;
            sample.BioMatSaliva = other.BioMatSaliva;
            sample.BioMatBuccalSwabs = other.BioMatBuccalSwabs;
            sample.BioMatAcidCitrateDextrose = other.BioMatAcidCitrateDextrose;
            sample.BioMatSynovialFluid = other.BioMatSynovialFluid;
            sample.BioMatSynovialTissue = other.BioMatSynovialTissue;
            sample.BioMatSerumSeparatorTube = other.BioMatSerumSeparatorTube;
            sample.BioMatPlasmaSeparatorTube = other.BioMatPlasmaSeparatorTube;
            sample.BioMatUrine = other.BioMatUrine;
            sample.BioMatOtherTubes = other.BioMatOtherTubes;
            sample.BioMatEdtaBlood = other.BioMatEdtaBlood;
            sample.BioMatSalivaNoAdditive = other.BioMatSalivaNoAdditive;
            sample.BioMatSalivaOragene = other.BioMatSalivaOragene;
            sample.BioMatCulture = other.BioMatCulture;
            sample.BioMatUnknown = other.BioMatUnknown;
            sample.BioMatOther = other.BioMatOther;
            sample.BioMatOtherTubesSpecify = other.BioMatOtherTubesSpecify;
            sample.BioMatCultureSpecify = other.BioMatCultureSpecify;
            sample.BioMatOtherSpecify = other.BioMatOtherSpecify;
            sample.TissueSamplesPreserved = other.TissueSamplesPreserved;
            sample.TissueSamplesPreservedSpecify = other.TissueSamplesPreservedSpecify;
            sample.SampleVolume = other.SampleVolume;
            sample.SampleVolumeSpecify = other.SampleVolumeSpecify;
            sample.CellCount = other.CellCount;
            sample.Concentration = other.Concentration;
            sample.ConcentrationSpecify = other.ConcentrationSpecify;
            sample.NumberOfAliquots = other.NumberOfAliquots;
            sample.WhenCollected = other.WhenCollected;
            sample.SnapFrozen = other.SnapFrozen;
            sample.HowDnaExtracted = other.HowDnaExtracted;
            sample.HowDnaExtractedSpecify = other.HowDnaExtractedSpecify;
            sample.TimeBetweenCollectionAndStorage = other.TimeBetweenCollectionAndStorage;
            sample.CollectionToStorageTemp = other.CollectionToStorageTemp;
            sample.CollectionToStorageTempSpecify = other.CollectionToStorageTempSpecify;
            sample.StorageTemp = other.StorageTemp;
            sample.StorageTempSpecify = other.StorageTempSpecify;
            sample.AlwayStoredAtThisTemp = other.AlwayStoredAtThisTemp;
            sample.DnaQualityAbsorbance = other.DnaQualityAbsorbance;
            sample.DnaQualityGel = other.DnaQualityGel;
            sample.DnaQualityCommercialKit = other.DnaQualityCommercialKit;
            sample.DnaQualityPcr = other.DnaQualityPcr;
            sample.DnaQualityPicoGreen = other.DnaQualityPicoGreen;
            sample.DnaQualityUnknown = other.DnaQualityUnknown;
            sample.DnaQualityOther = other.DnaQualityOther;
            sample.DnaQualityOtherSpecify = other.DnaQualityOtherSpecify;
            sample.FreezeThawCycles = other.FreezeThawCycles;
            sample.AnalysisNo = other.AnalysisNo;
            sample.AnalysisSequencing = other.AnalysisSequencing;
            sample.AnalysisRealTimePcr = other.AnalysisRealTimePcr;
            sample.AnalysisPcr = other.AnalysisPcr;
            sample.AnalysisGenotyping = other.AnalysisGenotyping;
            sample.AnalysisBiochemistry = other.AnalysisBiochemistry;
            sample.AnalysisImmunochemistry = other.AnalysisImmunochemistry;
            sample.AnalysisDnaExtraction = other.AnalysisDnaExtraction;
            sample.AnalysisImmunohistochemistry = other.AnalysisImmunohistochemistry;
            sample.AnalysisCellLinesDerived = other.AnalysisCellLinesDerived;
            sample.AnalysisRnaExtraction = other.AnalysisRnaExtraction;
            sample.AnalysisUnknown = other.AnalysisUnknown;
            sample.AnalysisOther = other.AnalysisOther;
            sample.AnalysisOtherSpecify = other.AnalysisOtherSpecify;
            sample.DnaExtracted = other.DnaExtracted;
            sample.CellsGrown = other.CellsGrown;
        }

        public static void UpdateDataFields(this Study study, bool useTimePoints, IEnumerable<StudyDataItemModel> dataitems, 
            IDataItemRepository dataItemRepo)
        {
            study.UseTimePoints = useTimePoints;
            if (null == dataitems)
            {
                study.DataItems.Clear();
                return;
            }

            //remove deleted data fields
            foreach (
                var di in study.DataItems.Where(sdi => !dataitems.Select(m => m.Id).Contains(sdi.DataItem.Id)).ToList())
            {
                study.DataItems.Remove(di);
            }

            //add new data fields
            foreach (var di in dataitems.Where(m => !study.DataItems.Select(sdi => sdi.DataItem.Id).Contains(m.Id)).ToList())
            {
                DataItem dataItem;
                if (di.Id > 0)
                {
                    dataItem = dataItemRepo.Get(di.Id);
                }
                else
                {
                    dataItem = dataItemRepo.Find("DataItemName", di.DataItemName).FirstOrDefault() ??
                               new DataItem {DataItemName = di.DataItemName};
                }
                var studyDataItem = new StudyDataItem
                                        {
                                            DataItem = dataItem
                                        };
                studyDataItem.UpdateStudyDataItemTimePoints(study, useTimePoints, di.TimePoints);
                study.DataItems.Add(studyDataItem);
            }

            //update existing data fields
            foreach (var di in dataitems.Where(m => study.DataItems.Select(sdi => sdi.DataItem.Id).Contains(m.Id)))
            {
                var studyDataItem = study.DataItems.Single(x => x.DataItem.Id == di.Id);
                studyDataItem.UpdateStudyDataItemTimePoints(study, useTimePoints, di.TimePoints);

            }
        }

        public static void UpdateStudyDataItemTimePoints(this StudyDataItem studyDataItem, Study study, bool useTimePoints, int[] timePoints)
        {
            if (!useTimePoints )
            {
                studyDataItem.TimePoints.Clear();
                return;
            }

            //remove deleted time points
            foreach (var tp in studyDataItem.TimePoints.Where(stp => !timePoints.Contains(stp.Id)).ToList())
            {
                studyDataItem.TimePoints.Remove(tp);
            }

            //add new time points
            foreach (var tp in timePoints.Where(m => !studyDataItem.TimePoints.Select(stp => stp.Id).Contains(m)))
            {
                studyDataItem.TimePoints.Add(study.TimePoints.Single(x => x.Id == tp));
            }
        }
    }
}