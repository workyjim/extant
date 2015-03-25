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
    public class StudyAccessControlAttribute : AuthorizeAttribute
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
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            if (IsOwnerOrHubLead(new StudyRequest(filterContext, IdRouteParameter)))
            {
                SetCachePolicy(filterContext);
            }
            else
            {
                filterContext.Result = new ViewResult {ViewName = "Unauthorized"};
            }
        }

        protected bool IsOwnerOrHubLead(StudyRequest request)
        {
            var userRepo = ObjectFactory.GetInstance<IUserRepository>();
            return userRepo.CanEditStudy(request.StudyId, request.Username, HubLeadRole);
        }

        protected override HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            var status = base.OnCacheAuthorization(httpContext);
            if (status == HttpValidationStatus.IgnoreThisRequest && IsOwnerOrHubLead(new StudyRequest(httpContext, IdRouteParameter)))
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

    public class StudyRequest
    {
        public int StudyId { get; set; }
        public string Username { get; set; }
        public bool IsAdministrator { get; set; }

        public StudyRequest(AuthorizationContext filterContext, string routeParameter)
        {
            if (filterContext.RouteData.Values.ContainsKey(routeParameter))
            {
                StudyId = Convert.ToInt32(filterContext.RouteData.Values[routeParameter]);
            }
            Username = filterContext.HttpContext.User.Identity.Name;
            IsAdministrator = filterContext.HttpContext.User.IsInRole(Constants.AdministratorRole);
        }

        public StudyRequest(HttpContextBase httpContext, string routeParameter)
        {
            if (!string.IsNullOrEmpty(routeParameter))
            {
                if (httpContext.Request.Params[routeParameter] != null)
                {
                    StudyId = GetStudyId(httpContext.Request.Params[routeParameter]);
                }
                else if (string.Equals("_id", routeParameter, StringComparison.OrdinalIgnoreCase))
                {
                    // id may be last element in path if not included as a parameter
                    StudyId = GetStudyId(httpContext.Request.Path.Split('/').Last());
                }
            }
            Username = httpContext.User.Identity.Name;
            IsAdministrator = httpContext.User.IsInRole(Constants.AdministratorRole);
        }

        private static int GetStudyId(string id)
        {
            int studyId;
            if (!int.TryParse(id, out studyId))
            {
                studyId = -1;
            }
            return studyId;
        }

    }
}