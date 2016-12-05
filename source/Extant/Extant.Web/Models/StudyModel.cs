//-----------------------------------------------------------------------
// <copyright file="StudyModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using DataAnnotationsExtensions;
using Extant.Data.Entities;
using Extant.Web.Infrastructure;

namespace Extant.Web.Models
{
    public class StudyModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Study Name")]
        [HelpText("The name that the study is commonly known by. If the study is usually referred by a shortened or abbreviated name then use that here and include the full official name in the Description below.")]
        public string StudyName { get; set; }

        [Required]
        [StringLength(4000)]
        [Display(Name = "Description")]
        [HelpText("A short abstract that describes the study")]
        public string Description { get; set; }

        [StringLength(255)]
        [Display(Name = "Synonyms")]
        [HelpText("Any other names that the study is commonly known as, abbrevations etc.")]
        public string StudySynonyms { get; set; }

        [DataAnnotationsExtensions.Url]
        [Display(Name = "Study Website")]
        [HelpText("The full address of the study's website (optional)")]
        public string StudyWebsite { get; set; }

        [Required]
        [Display(Name = "Study Design")]
        [HelpText("The design of the study; observational or interventional")]
        public int StudyDesign { get; set; }

        public IEnumerable<int> DiseaseAreas { get; set; }

        [Display(Name = "Start Date")]
        [HelpText("The date when recruitment to the study started (or will start)")]
        public DateTime? StartDate { get; set; }

        [Required]
        [Display(Name = "Study Status")]
        [HelpText("The status of the study in terms of recruitment and follow up of patients")]
        public int StudyStatus { get; set; }

        [Integer]
        [Display(Name = "Recruitment Target")]
        [HelpText("The number of patients that is planned to be recruited to the study.")]
        public int? RecruitmentTarget { get; set; }

        [Integer]
        [Display(Name="Participants Recruited")]
        [HelpText("The number of patients that have been recruited to the study to date. This should be updated periodically.")]
        public int? ParticipantsRecruited { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Chief Investigator")]
        [HelpText("The name of the Chief Investigator of the study.")]
        public string PrincipalInvestigator { get; set; }

        [StringLength(255)]
        [Display(Name = "Institution")]
        [HelpText("The name of the main institution involved in the running of the study.")]
        public string Institution { get; set; }

        [StringLength(255)]
        [Display(Name = "Funder")]
        [HelpText("The name of the main funder of the study (or the most relevant if there are more than one).")]
        public string Funder { get; set; }

        [Required]
        [Display(Name = "Is the study adopted on the UKCRN Portfolio?")]
        public bool OnPortfolio { get; set; }

        [Integer]
        [RequiredIf("OnPortfolio", true)]
        [Display(Name = "UKCRN ID")]
        [HelpText("The ID of the study in the UKCRN Portfolio.")]
        public int? PortfolioNumber { get; set; }

        [StringLength(255)]
        [Required]
        [Display(Name = "Name")]
        [HelpText("The name of the primary contact for obtaining further information on this study.")]
        public string ContactName { get; set; }

        [StringLength(4000)]
        [Required]
        [Display(Name = "Address")]
        [HelpText("The postal address of the primary contact for obtaining further information on this study.")]
        public string ContactAddress { get; set; }

        [StringLength(255)]
        [Display(Name = "Phone")]
        [HelpText("The telephone number of the primary contact for obtaining further information on this study.")]
        public string ContactPhone { get; set; }

        [Required]
        [Email]
        [StringLength(255)]
        [Display(Name = "Email")]
        [HelpText("The email address of the primary contact for obtaining further information on this study.")]
        public string ContactEmail { get; set; }

        //[FileExtensions("doc,docx,pdf,txt,rdf")]
        [Display(Name = "Patient Information Leaflet")]
        [HelpText("Select a file on your computer to upload the patient information leaflet for the study. If there are multiple patient information leaflets then the others can be added in the Additional Files section below.")]
        public HttpPostedFileBase PatientInformationLeaflet { get; set; }

        public bool PatientInformationLeafletRemoved { get; set; }

        public FileUploadModel PatientInformationLeafletCurrent { get; set; }

        //[FileExtensions("doc,docx,pdf,txt,rdf")]
        [Display(Name = "Consent Form")]
        [HelpText("Select a file on your computer to upload the consent form for the study. If there are multiple consent forms then the others can be added in the Additional Files section below.")]
        public HttpPostedFileBase ConsentForm { get; set; }

        public bool ConsentFormRemoved { get; set; }

        public FileUploadModel ConsentFormCurrent { get; set; }

        [Display(Name = "Does the study have a Data Access Policy?")]
        public bool HasDataAccessPolicy { get; set; }

        //[FileExtensions("doc,docx,pdf,txt,rdf")]
        [Display(Name = "Data Access Policy")]
        [HelpText("Select a file on your computer to upload the data access policy for the study.")]
        public HttpPostedFileBase DataAccessPolicy { get; set; }

        public bool DataAccessPolicyRemoved { get; set; }

        public FileUploadModel DataAccessPolicyCurrent { get; set; }

        public IEnumerable<AdditionalDocumentModel> AdditionalDocuments { get; set; }

        [Required]
        [Display(Name="Is the study longitudinal")]
        public bool IsLongitudinal { get; set; }

        public IEnumerable<TimePointModel> TimePoints { get; set; }

        public IEnumerable<DiseaseArea> AllDiseaseAreas { get; set; }

        public bool IsNew { get; set; }
    }
}