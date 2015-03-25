//-----------------------------------------------------------------------
// <copyright file="AdditionalDocumentDisplayModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Extant.Web.Models
{
    public class AdditionalDocumentDisplayModel
    {
        public virtual string Description { get; set; }

        public virtual string DocumentType { get; set; }

        public virtual FileUploadModel File { get; set; }
    }
}