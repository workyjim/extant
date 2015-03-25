//-----------------------------------------------------------------------
// <copyright file="Study.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Extant.Data.Search;
using NHibernate.Search.Attributes;

namespace Extant.Data.Entities
{
    [Indexed]
    [ClassBridge(typeof(StudyFieldBridge), Name = "AllFields", Index = Index.Tokenized, Store = Store.Yes)]
    [ClassBridge(typeof(StudySamplesFieldBridge), Name = "Samples", Index = Index.Tokenized, Store = Store.Yes)]
    [ClassBridge(typeof(StudyNameForSortFieldBridge), Name = "StudyNameForSort", Index = Index.UnTokenized, Store = Store.Yes)]
    public class Study : Entity
    {
        public virtual User Owner { get; set; }

        private IList<User> editors = new List<User>();
        public virtual IList<User> Editors
        {
            get { return editors; }
            set { editors = value; }
        }

        public virtual DateTime StudyAdded { get; set; }

        public virtual DateTime StudyUpdated { get; set; }

        [Field(Index.UnTokenized, Store = Store.Yes)]
        [FieldBridge(typeof(BoolBridge))]
        public virtual bool Published { get; set; }

        #region Basic Details

        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string StudyName { get; set; }

        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string Description { get; set; }

        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string StudySynonyms { get; set; }

        public virtual string StudyWebsite { get; set; }

        private IList<DiseaseArea> diseaseAreas = new List<DiseaseArea>();
        [IndexedEmbedded]
        public virtual IList<DiseaseArea> DiseaseAreas
        {
            get { return diseaseAreas; }
            set { diseaseAreas = value; }
        }

        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual StudyDesign StudyDesign { get; set; }

        public virtual DateTime? StartDate { get; set; }

        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual StudyStatus StudyStatus { get; set; }

        public virtual int? RecruitmentTarget { get; set; }

        public virtual int? ParticipantsRecruited { get; set; }

        public virtual DateTime ParticipantsRecruitedUpdated { get; set; }

        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string PrincipalInvestigator { get; set; }

        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string Institution { get; set; }

        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string Funder { get; set; }

        public virtual bool OnPortfolio { get; set; }

        public virtual int? PortfolioNumber { get; set; }

        public virtual string ContactName { get; set; }

        public virtual string ContactAddress { get; set; }

        public virtual string ContactPhone { get; set; }

        public virtual string ContactEmail { get; set; }

        public virtual FileUpload PatientInformationLeaflet { get; set; }

        public virtual FileUpload ConsentForm { get; set; }

        public virtual bool HasDataAccessPolicy { get; set; }

        public virtual FileUpload DataAccessPolicy { get; set; }

        private IList<AdditionalDocument> additionalDocuments = new List<AdditionalDocument>();
        public virtual IList<AdditionalDocument> AdditionalDocuments
        {
            get { return additionalDocuments; }
            set { additionalDocuments = value; }
        }

        public virtual bool IsLongitudinal { get; set; }

        private IList<TimePoint> timePoints = new List<TimePoint>();
        public virtual IList<TimePoint> TimePoints
        {
            get { return timePoints; }
            set { timePoints = value; }
        }

        public virtual bool UseTimePoints { get; set; }

        #endregion

        #region Samples

        public virtual NumberOfSamples NumberOfDnaSamples { get; set; }

        public virtual int? NumberOfDnaSamplesExact { get; set; }

        public virtual NumberOfSamples NumberOfSerumSamples { get; set; }

        public virtual int? NumberOfSerumSamplesExact { get; set; }

        public virtual NumberOfSamples NumberOfPlasmaSamples { get; set; }

        public virtual int? NumberOfPlasmaSamplesExact { get; set; }

        public virtual NumberOfSamples NumberOfWholeBloodSamples { get; set; }

        public virtual int? NumberOfWholeBloodSamplesExact { get; set; }

        public virtual NumberOfSamples NumberOfSalivaSamples { get; set; }

        public virtual int? NumberOfSalivaSamplesExact { get; set; }

        public virtual NumberOfSamples NumberOfTissueSamples { get; set; }

        public virtual int? NumberOfTissueSamplesExact { get; set; }

        public virtual NumberOfSamples NumberOfCellSamples { get; set; }

        public virtual int? NumberOfCellSamplesExact { get; set; }

        public virtual NumberOfSamples NumberOfOtherSamples { get; set; }

        public virtual int? NumberOfOtherSamplesExact { get; set; }

        public virtual bool DetailedSampleInfo { get; set; }

        private IList<Sample> samples = new List<Sample>();
        public virtual IList<Sample> Samples
        {
            get { return samples; }
            set { samples = value; }
        }

        #endregion

        private IList<Publication> publications = new List<Publication>();
        [IndexedEmbedded]
        public virtual IList<Publication> Publications
        {
            get { return publications; }
            set { publications = value; }
        }

        private IList<StudyDataItem> dataItems = new List<StudyDataItem>();
        [IndexedEmbedded]
        public virtual IList<StudyDataItem> DataItems
        {
            get { return dataItems; }
            set { dataItems = value; }
        }

    }

    public enum StudyDesign
    {
        Observational = 1,
        Interventional = 2
    }

    public enum StudyStatus
    {
        InSetUp = 1,
        Recruiting = 2,
        [Description("Recruitment Complete - No Follow Up")]
        RecruitmentCompleteNoFollowUp = 3,
        [Description("Recruitment Complete - Follow Up Ongoing")]
        RecruitmentCompleteFollowUpOngoing = 4,
        RecruitmentAndFollowUpComplete = 5
    }

    public enum NumberOfSamples
    {
        None = 0,
        [Description("1-100")]
        N1To100 = 1,
        [Description("101-250")]
        N101To250 = 2,
        [Description("251-500")]
        N251To500 = 3,
        [Description("501-1000")]
        N501To1k = 4,
        [Description("1000-5000")]
        N1kTo5k = 5,
        [Description("5000-10000")]
        N5kTo10k = 6,
        [Description("10000+")]
        N10kPlus = 7,
        [Description("Exact Number If Known")]
        ExactNumber = 8,
        Unknown = 9
    }

}