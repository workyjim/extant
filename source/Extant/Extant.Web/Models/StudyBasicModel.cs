//-----------------------------------------------------------------------
// <copyright file="StudyBasicModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Extant.Web.Models
{
    public class StudyBasicModel
    {
        public int Id { get; set; }

        public string StudyName { get; set; }

        public string Description { get; set; }

        public bool Published { get; set; }

        public bool CanDelete { get; set; }

        public virtual UserBasicModel Owner { get; set; }
    }
}