//-----------------------------------------------------------------------
// <copyright file="FileUploadConverter.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Web;
using AutoMapper;
using Extant.Data.Entities;

namespace Extant.Web.Infrastructure
{
    public class FileUploadConverter : TypeConverter<HttpPostedFileBase, FileUpload>
    {
        protected override FileUpload ConvertCore(HttpPostedFileBase source)
        {
            if (null != source && source.ContentLength > 0)
            {
                var fileUpload = new FileUpload();
                fileUpload.FileData = new byte[source.ContentLength];
                source.InputStream.Read(fileUpload.FileData, 0, source.ContentLength);
                fileUpload.FileName = source.FileName;
                fileUpload.FileSize = source.ContentLength;
                fileUpload.MimeType = source.ContentType;
                return fileUpload;
            }
            return null;
        }
    }
}