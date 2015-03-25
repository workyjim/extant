//-----------------------------------------------------------------------
// <copyright file="FileUploadModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Extant.Web.Models
{
    public class FileUploadModel
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string FileSize { get; set; }

        public string MimeType { get; set; }
    }
}