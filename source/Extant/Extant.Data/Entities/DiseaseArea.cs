//-----------------------------------------------------------------------
// <copyright file="DiseaseArea.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using NHibernate.Search.Attributes;

namespace Extant.Data.Entities
{
    [Indexed]
    public class DiseaseArea : Entity
    {
        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string DiseaseAreaName { get; set; }

        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string DiseaseAreaSynonyms { get; set; }

        public virtual bool Published { get; set; }

        private IList<Category> categories = new List<Category>();
        public virtual IList<Category> Categories
        {
            get { return categories; }
            set { categories = value; }
        }
    }
}