//-----------------------------------------------------------------------
// <copyright file="StudyNameForSortFieldBridge.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using Extant.Data.Entities;
using Lucene.Net.Documents;
using NHibernate.Search.Bridge;

namespace Extant.Data.Search
{
    public class StudyNameForSortFieldBridge : IFieldBridge
    {
        public void Set(string name, object value, Document document, Field.Store store, Field.Index index, float? boost)
        {
            var study = (Study)value;
            var field = new Field(name, study.StudyName.ToLower(), store, index);
            field.SetBoost(boost ?? 1);
            document.Add(field);
        }
    }
}