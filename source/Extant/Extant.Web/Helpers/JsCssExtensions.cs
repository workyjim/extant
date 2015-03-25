//-----------------------------------------------------------------------
// <copyright file="JsCssExtensions.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Reflection;
using System.Web.Mvc;

namespace Extant.Web.Helpers
{
    public static class JsCssExtensions
    {
        static readonly string Version;

        static JsCssExtensions()
        {
            Version = new AssemblyName(typeof(JsCssExtensions).Assembly.FullName).Version.ToString();
        }

        public static string VersionedContent(this UrlHelper urlHelper, string file)
        {
            return urlHelper.Content(string.Format("{0}?v={1}", file, Version));
        }
    }
}