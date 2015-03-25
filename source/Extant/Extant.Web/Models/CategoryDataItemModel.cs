//-----------------------------------------------------------------------
// <copyright file="CategoryDataItemModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Extant.Web.Models
{
    public class CategoryDataItemModel
    {
        public int Id { get; set; }

        public string ShortName { get; set; }

        public DataItemModel DataItem { get; set; }
    }
}