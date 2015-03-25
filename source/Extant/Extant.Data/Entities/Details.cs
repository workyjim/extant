//-----------------------------------------------------------------------
// <copyright file="Details.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.ComponentModel;
using Extant.Web.Forms;
using NHibernate.Search.Attributes;

namespace Extant.Data.Entities
{
    [Indexed]
    public class Details : Entity
    {
        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string StudyName { get; set; }

        [MultiSelect]
        public virtual DiseaseArea DiseaseArea { get; set; }

        [MultiSelect]
        public virtual StudyDesign StudyDesign { get; set; }

        public virtual DateTime? StartDate { get; set; }

        [Optional]
        public virtual DateTime? EndDate { get; set; }

        [Optional]
        public virtual int? NumberOfParticipants { get; set; }

        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string PrincipalInvestigator { get; set; }

        public virtual string ContactName { get; set; }

        public virtual string ContactAddress { get; set; }

        public virtual string ContactTown { get; set; }

        public virtual string ContactPostcode { get; set; }

        [Optional]
        public virtual string ContactPhone { get; set; }

        public virtual string ContactEmail { get; set; }

        [Optional]
        public virtual FileUpload PatientInformationLeaflet { get; set; }

        [Optional]
        public virtual FileUpload ConsentForm { get; set; }
    }

    [TypeConverter(typeof(EnumTypeConverter))]
    public enum DiseaseAreaEnum
    {
        [Description("Rheumatoid Arthritis")]
        RheumatoidArthritis = 1,
        [Description("Osteoarthritis")]
        Osteoarthritis = 2,
        [Description("Psoriatic Arthritis")]
        PsoriaticArthritis = 3
    }

    public enum StudyDesign
    {
        Observational = 1,
        Interventional = 2
    }
}