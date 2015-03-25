//-----------------------------------------------------------------------
// <copyright file="ListStringFieldBridge.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using NHibernate.Search.Bridge;

namespace Extant.Data.Search
{
    public class ListStringFieldBridge : IStringBridge
    {
        public string ObjectToString(object obj)
        {
            if (null == obj) return null;
            return string.Join(" ", (IList<string>)obj);
        }
    }
}