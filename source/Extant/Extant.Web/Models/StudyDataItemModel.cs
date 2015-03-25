//-----------------------------------------------------------------------
// <copyright file="StudyDataItemModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Extant.Web.Models
{
    public class StudyDataItemModel
    {
        public int Id { get; set; }

        public string DataItemName { get; set; }

        public int[] TimePoints { get; set; }
    }
}