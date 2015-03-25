//-----------------------------------------------------------------------
// <copyright file="StudyIndexModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Extant.Web.Models
{
    public class StudyIndexModel
    {
        public bool CanEdit { get; set; }

        public int Id { get; set; }

        public string StudyUpdated { get; set; }

        public string StudyName { get; set; }

        public string Description { get; set; }

        public string StudySynonyms { get; set; }

        public string StudyWebsite { get; set; }

        public string DiseaseAreasText { get; set; }

        public IEnumerable<DiseaseAreaModel> DiseaseAreas { get; set; }

        public string StudyDesign { get; set; }

        public string StartDate { get; set; }

        public string StudyStatus { get; set; }

        public string RecruitmentTarget { get; set; }

        public string ParticipantsRecruited { get; set; }

        public string ParticipantsRecruitedUpdated { get; set; }

        public string PrincipalInvestigator { get; set; }

        public string Institution { get; set; }

        public string Funder { get; set; }

        public string OnPortfolio { get; set; }

        public string PortfolioNumber { get; set; }

        public string ContactName { get; set; }

        public string ContactAddress { get; set; }

        public string ContactPhone { get; set; }

        public string ContactEmail { get; set; }

        public string ContactEmailReversed { get; set; }

        public string ContactEmailEncoded { get; set; }

        public string IsLongitudinal { get; set; }

        public bool UseTimePoints { get; set; }

        public IEnumerable<TimePointModel> TimePoints { get; set; }

        public FileUploadModel PatientInformationLeaflet { get; set; }

        public FileUploadModel ConsentForm { get; set; }

        public string HasDataAccessPolicy { get; set; }

        public FileUploadModel DataAccessPolicy { get; set; }

        public IEnumerable<AdditionalDocumentDisplayModel> AdditionalDocuments { get; set; }

        public IEnumerable<PublicationModel> Publications { get; set; }

        public IEnumerable<StudyDataItemModel> DataItems { get; set; }

        public IDictionary<string, string> SampleNumbers { get; set; }

        public IEnumerable<SampleDisplayModel> Samples { get; set; }
    }
}