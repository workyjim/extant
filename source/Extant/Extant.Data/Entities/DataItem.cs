//-----------------------------------------------------------------------
// <copyright file="DataItem.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using NHibernate.Search.Attributes;

namespace Extant.Data.Entities
{
    [Indexed]
    public class DataItem : Entity
    {
        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string DataItemName { get; set; }

    }
}