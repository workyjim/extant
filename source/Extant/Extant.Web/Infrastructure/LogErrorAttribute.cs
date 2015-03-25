//-----------------------------------------------------------------------
// <copyright file="LogErrorAttribute.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Web.Mvc;
using log4net;

namespace Extant.Web.Infrastructure
{
    public class LogErrorAttribute : HandleErrorAttribute
    {
        protected readonly static ILog log = log4net.LogManager.GetLogger(typeof(LogErrorAttribute));

        public override void OnException(ExceptionContext filterContext)
        {
            log.Error("Error", filterContext.Exception);
        }
    }
}