//-----------------------------------------------------------------------
// <copyright file="FileUploadModelBinder.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Web.Mvc;
using Extant.Data.Entities;

namespace Extant.Web.Infrastructure
{
    public class FileUploadModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var theFile = controllerContext.HttpContext.Request.Files[bindingContext.ModelName];
            if ( null != theFile && theFile.ContentLength > 0 )
            {
                var fileUpload = new FileUpload();
                fileUpload.FileData = new byte[theFile.ContentLength];
                theFile.InputStream.Read(fileUpload.FileData, 0, theFile.ContentLength);
                fileUpload.FileName = theFile.FileName;
                fileUpload.MimeType = theFile.ContentType;
                return fileUpload;
            }
            return null;
        }
    }
}