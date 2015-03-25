//-----------------------------------------------------------------------
// <copyright file="CategoryModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Extant.Web.Models
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            Subcategories = new List<CategoryModel>();
            DataItems = new List<CategoryDataItemModel>();
        }

        public int Id { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<CategoryModel> Subcategories { get; set; }

        public IEnumerable<CategoryDataItemModel> DataItems { get; set; }
    }
}