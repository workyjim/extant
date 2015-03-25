//-----------------------------------------------------------------------
// <copyright file="StudyDataItem.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using Extant.Data.Search;
using NHibernate.Search.Attributes;

namespace Extant.Data.Entities
{
    [Indexed]
    [ClassBridge(typeof(StudyDataItemFieldBridge), Name = "DataItemName", Index = Index.Tokenized, Store = Store.Yes)]
    public class StudyDataItem : Entity
    {
        public virtual DataItem DataItem { get; set; }

        private IList<TimePoint> timePoints = new List<TimePoint>();
        public virtual IList<TimePoint> TimePoints
        {
            get { return timePoints; }
            set { timePoints = value; }
        }
    }
}