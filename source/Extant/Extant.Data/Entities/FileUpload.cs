//-----------------------------------------------------------------------
// <copyright file="FileUpload.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Extant.Data.Entities
{
    public class FileUpload : Entity
    {
        public virtual string FileName { get; set; }

        public virtual string MimeType { get; set; }

        public virtual int FileSize { get; set; }

        public virtual byte[] FileData { get; set; }
    }
}