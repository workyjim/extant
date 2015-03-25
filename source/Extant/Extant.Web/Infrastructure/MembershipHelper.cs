//-----------------------------------------------------------------------
// <copyright file="MembershipHelper.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace Extant.Web.Infrastructure
{
    public static class MembershipHelper
    {
        public static string GetConfigValue(string configValue, string defaultValue)
        {
            return String.IsNullOrEmpty(configValue) ? defaultValue : configValue;
        }
    }
}