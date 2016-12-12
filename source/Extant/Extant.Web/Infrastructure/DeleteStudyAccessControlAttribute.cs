//-----------------------------------------------------------------------
// <copyright file="StudyAccessControlAttribute.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Extant.Data;
using Extant.Data.Repositories;
using StructureMap;

namespace Extant.Web.Infrastructure
{
    /// <summary>
    /// See http://farm-fresh-code.blogspot.com/2009/11/customizing-authorization-in-aspnet-mvc.html
    /// and http://farm-fresh-code.blogspot.com/2011/03/revisiting-custom-authorization-in.html
    /// </summary>
    public class DeleteStudyAccessControlAttribute : AuthorizeAttribute
    {
        private string idRouteParameter = "_id";
        private string hubLeadRole = Constants.HubLeadRole;

        public string IdRouteParameter
        {
            get { return idRouteParameter; }
            set { idRouteParameter = value; }
        }

        public string HubLeadRole
        {
            get { return hubLeadRole; }
            set { hubLeadRole = value; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // auth failed, redirect to login page
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = 403;
                }
                else
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
                return;
            }

            if (CanDeleteStudy(new StudyRequest(filterContext, IdRouteParameter)))
            {
                SetCachePolicy(filterContext);
            }
            else
            {
                if ( filterContext.HttpContext.Request.IsAjaxRequest() )
                {
                    filterContext.HttpContext.Response.StatusCode = 403;
                } 
                else
                {
                    filterContext.Result = new ViewResult { ViewName = "Unauthorized" };                    
                }
            }
        }

        protected bool CanDeleteStudy(StudyRequest request)
        {
            var userRepo = (IUserRepository) DependencyResolver.Current.GetService(typeof(IUserRepository));
            return request.IsAdministrator || userRepo.CanDeleteStudy(request.StudyId, request.Username, HubLeadRole);
        }

        protected override HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            var status = base.OnCacheAuthorization(httpContext);
            if (status == HttpValidationStatus.IgnoreThisRequest && CanDeleteStudy(new StudyRequest(httpContext, IdRouteParameter)))
            {
                status = HttpValidationStatus.Valid;
            }
            return status;
        }

        protected virtual void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        protected virtual void SetCachePolicy(AuthorizationContext filterContext)
        {
            // ** IMPORTANT **
            // Since we're performing authorization at the action level, the authorization code runs
            // after the output caching module. In the worst case this could allow an authorized user
            // to cause the page to be cached, then an unauthorized user would later be served the
            // cached page. We work around this by telling proxies not to cache the sensitive page,
            // then we hook our custom authorization code into the caching mechanism so that we have
            // the final say on whether a page should be served from the cache.
            HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
            cachePolicy.SetProxyMaxAge(new TimeSpan(0));
            cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
        }
    }

}