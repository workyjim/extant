//-----------------------------------------------------------------------
// <copyright file="AttributeExtensions.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2009. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Extant.Web.Helpers
{
    public static class AttributeExtensions
    {
        public static bool AttributeExists<T>(this MemberInfo propertyInfo) where T : Attribute
        {
            var attribute = propertyInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
            if (attribute == null)
            {
                return false;
            }
            return true;
        }

        public static T GetAttribute<T>(this MemberInfo propertyInfo) where T : Attribute
        {
            return propertyInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        }

        public static IEnumerable<T> GetAttributes<T>(this MemberInfo propertyInfo) where T : Attribute
        {
            return propertyInfo.GetCustomAttributes(typeof(T), false).Cast<T>();
        }

    }
}