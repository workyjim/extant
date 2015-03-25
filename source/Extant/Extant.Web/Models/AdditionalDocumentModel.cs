//-----------------------------------------------------------------------
// <copyright file="AdditionalDocumentModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Web;

namespace Extant.Web.Models
{
    public class AdditionalDocumentModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int DocumentType { get; set; }

        public HttpPostedFileBase File { get; set; }

        public FileUploadModel FileCurrent { get; set; }

        public bool FileRemoved { get; set; }
    }
}