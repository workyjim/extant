//-----------------------------------------------------------------------
// <copyright file="StudyDataItemFieldBridge.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using Extant.Data.Entities;
using Lucene.Net.Documents;
using NHibernate.Search.Bridge;

namespace Extant.Data.Search
{
    public class StudyDataItemFieldBridge : IFieldBridge
    {
        public void Set(string name, object value, Document document, Field.Store store, Field.Index index, float? boost)
        {
            var sdi = value as StudyDataItem;
            if (null == sdi) return;
            var field = new Field(name, sdi.DataItem.DataItemName, store, index);
            field.SetBoost(boost ?? 1);
            document.Add(field);
        }
    }
}