using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Extant.Web.Infrastructure;
using StructureMap;

namespace Extant.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new NHibernateActionFilter());
            filters.Add(new LogErrorAttribute());
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{_id}", // URL with parameters
                new { controller = "Home", action = "Index", _id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            Bootstrapper.Bootstrap();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            ModelMetadataProviders.Current = new FormHelpModelMetadataProvider();
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Application_BeginRequest()
        {

            StructureMapDependencyResolver r = (StructureMapDependencyResolver)DependencyResolver.Current;
            IContainer main = (IContainer)DependencyResolver.Current.GetService(typeof(IContainer));

            // create a nested container for this httpcontext which will be used to resolve dependencies
            // this allows StructureMap dependecies to use standard transient scoping instead of special http scoping.
            HttpContext.Current.Items[StructureMapDependencyResolver.ScopedContainerKey] = main.GetNestedContainer();
        }

        protected void Application_EndRequest()
        {
            // explicitly dispose of nexted container at end of request
            IContainer nested = (IContainer)HttpContext.Current.Items[StructureMapDependencyResolver.ScopedContainerKey];
            nested.Dispose();
        }
    }
}