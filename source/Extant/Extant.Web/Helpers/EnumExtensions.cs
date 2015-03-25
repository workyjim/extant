//-----------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Extant.Web.Helpers
{
    public static class EnumExtensions
    {
        public static string EnumToString<TEnum>(this TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if ((attributes != null) && (attributes.Length > 0))
                return attributes[0].Description;
            else
                return value.ToString().SplitToWordsOnCapitals();
        }

        private static string SplitToWordsOnCapitals(this string value)
        {
            return Regex.Replace(value, "((?<=[a-z])[A-Z]|[A-Z](?=[a-z]))", " $1").Trim();
        }
    }
}