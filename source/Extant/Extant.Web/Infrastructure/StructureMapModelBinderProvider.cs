//-----------------------------------------------------------------------
// <copyright file="StructureMapModelBinderProvider.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Web.Mvc;
using StructureMap;

namespace Extant.Web.Infrastructure
{
    /// <summary>
    /// See http://www.thecodinghumanist.com/blog/archives/2011/1/25/structuremap-model-binders-and-dependency-injection-in-asp-net-mvc-3
    /// </summary>
    public class StructureMapModelBinderProvider : IModelBinderProvider
    {
        private readonly IContainer _container;

        public StructureMapModelBinderProvider(IContainer container)
        {
            _container = container;
        }

        public IModelBinder GetBinder(Type modelType)
        {
            var typeMappings = _container.GetInstance<ModelBinderTypeMappingDictionary>();
            if (typeMappings != null && typeMappings.ContainsKey(modelType))
            {
                var binderType = typeMappings[modelType];
                var binder = _container.GetInstance(binderType);
                return binder as IModelBinder;
            }
            return null;
        }
    }
}