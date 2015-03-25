//-----------------------------------------------------------------------
// <copyright file="StudiesByDataItemModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Extant.Web.Models
{
    public class StudiesByDataItemModel
    {
        public string DataItemName { get; set; }
        public IEnumerable<StudyBasicModel> Studies { get; set; }
    }
}