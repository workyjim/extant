//-----------------------------------------------------------------------
// <copyright file="StudyEditorsModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Extant.Web.Models
{
    public class StudyEditorsModel
    {
        public int Id { get; set; }

        public string StudyName { get; set; }

        public IEnumerable<UserBasicModel> Editors { get; set; }
    }
}