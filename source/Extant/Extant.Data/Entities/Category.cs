//-----------------------------------------------------------------------
// <copyright file="Category.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Extant.Data.Entities
{
    public class Category : Entity
    {
        public virtual string CategoryName { get; set; }

        private IList<Category> subcategories = new List<Category>();
        public virtual IList<Category> Subcategories
        {
            get { return subcategories; }
            set { subcategories = value; }
        }

        private IList<CategoryDataItem> dataItems = new List<CategoryDataItem>();
        public virtual IList<CategoryDataItem> DataItems
        {
            get { return dataItems; }
            set { dataItems = value; }
        }
    }
}