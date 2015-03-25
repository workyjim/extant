//-----------------------------------------------------------------------
// <copyright file="AdditionalDocument.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Extant.Data.Entities
{
    public class AdditionalDocument : Entity
    {
        public virtual string Description { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual FileUpload File { get; set; }
    }

    public enum DocumentType
    {
        Other = 0,
        ConsentForm = 1,
        AssentForm = 2,
        PatientInformationLeaflet = 3
    }
}