//-----------------------------------------------------------------------
// <copyright file="PrincipalModelBinder.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Security.Principal;
using System.Web.Mvc;

namespace Extant.Web.Infrastructure
{
    public class PrincipalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (bindingContext == null)
            {
                throw new ArgumentNullException("bindingContext");
            }
            return controllerContext.HttpContext.User;
        }
    }
}