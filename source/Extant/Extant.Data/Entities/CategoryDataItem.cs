//-----------------------------------------------------------------------
// <copyright file="CategoryDataItem.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Extant.Data.Entities
{
    public class CategoryDataItem : Entity
    {
        public virtual string ShortName { get; set; }

        public virtual DataItem DataItem { get; set; }
    }
}