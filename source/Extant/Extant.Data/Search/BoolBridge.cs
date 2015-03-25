//-----------------------------------------------------------------------
// <copyright file="BoolBridge.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2013. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using Lucene.Net.Documents;
using NHibernate.Search.Bridge;

namespace Extant.Data.Search
{
    public class BoolBridge : IFieldBridge
    {
        public void Set(string name, object value, Document document, Field.Store store, Field.Index index, float? boost)
        {
            var field = new Field(name, value.ToString().ToLower(), store, index);
            field.SetBoost(boost ?? 1);
            document.Add(field);
        }
    }
}