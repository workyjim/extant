//-----------------------------------------------------------------------
// <copyright file="RequireQueryStringValueAttribute.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Extant.Web.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RequireQueryStringValueAttribute : ActionMethodSelectorAttribute
    {
        public RequireQueryStringValueAttribute(string valueName)
        {
            ValueName = valueName;
        }
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.HttpContext.Request.QueryString.AllKeys.Contains(ValueName);
        }
        public string ValueName { get; private set; }
    }

}