//-----------------------------------------------------------------------
// <copyright file="StructureMapDependencyResolver.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace Extant.Web.Infrastructure
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        private IContainer getCurrentContainer()
        {
            // detects if a context-scoped nested container is available; if not uses the global container.
            // using this methods of scoping allows all registrations to be transient or singleton.
            return (IContainer) (System.Web.HttpContext.Current.Items[ScopedContainerKey] ?? _container);
        }
        

        public StructureMapDependencyResolver(IContainer container)
        {
            _container = container;
        }

        // context scoping key for per request nested containers.
        public const string ScopedContainerKey = "StructureMapRequestScopedContainer";

        public object GetService(Type serviceType)
        {

            IContainer current = getCurrentContainer();
            
            if (serviceType == typeof(IContainer)) return current;

            object instance = current.TryGetInstance(serviceType);

            if (instance == null && !serviceType.IsAbstract)
            {
                current.Configure(c => c.AddType(serviceType, serviceType));
                instance = current.TryGetInstance(serviceType);
            }

            return instance;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return getCurrentContainer().GetAllInstances(serviceType).Cast<object>();
        }
    }
}