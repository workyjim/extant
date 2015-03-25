//-----------------------------------------------------------------------
// <copyright file="StudyPublicationsModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Extant.Web.Models
{
    public class StudyPublicationsModel
    {
        public int Id { get; set; }

        public string StudyName { get; set; }

        public IEnumerable<PublicationModel> Publications { get; set; }

        public bool IsNew { get; set; }
    }
}