//-----------------------------------------------------------------------
// <copyright file="DateTimeModelBinder.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Web.Mvc;

namespace Extant.Web.Infrastructure
{
    public class DateTimeModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (null != value.AttemptedValue)
            {
                try
                {
                    //try timestamp date format
                    return DateTime.ParseExact(value.AttemptedValue, "yyyy-MM-dd hh:mm:ss.zzz", null);
                }
                catch (Exception ex)
                {
                    //if timestamp date format failed, try date only format
                    return DateTime.ParseExact(value.AttemptedValue, "dd-MM-yyyy", null);
                }
            }
            return null;
        }
    }
}