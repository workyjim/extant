//-----------------------------------------------------------------------
// <copyright file="NHibernateActionFilter.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Web.Mvc;
using Extant.Data;
using StructureMap;

namespace Extant.Web.Infrastructure
{
    /// <summary>
    /// See http://ayende.com/blog/4809/refactoring-toward-frictionless-odorless-code-what-about-transactions
    /// </summary>
    public class NHibernateActionFilter : ActionFilterAttribute
    {
        public const string UowKey = "uow";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Items.Add(UowKey, ObjectFactory.GetInstance<IUnitOfWork>());
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var unitOfWork = GetUnitOfWork(filterContext);
            if (unitOfWork == null) return;
            if (filterContext.Exception != null)
            {
                unitOfWork.Rollback();
            }
            else
            {
                unitOfWork.Commit();
            }
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var unitOfWork = GetUnitOfWork(filterContext);
            unitOfWork.Dispose();
        }

        static IUnitOfWork GetUnitOfWork(ControllerContext context)
        {
            return (IUnitOfWork)context.HttpContext.Items[UowKey];
        }
    }
}