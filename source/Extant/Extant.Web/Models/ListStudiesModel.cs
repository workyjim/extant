//-----------------------------------------------------------------------
// <copyright file="ListStudiesModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using NWeH.Paging;

namespace Extant.Web.Models
{
    public class ListStudiesModel
    {
        public bool IsHubLead { get; set; }

        public IPagedList<StudyBasicModel> MyStudies { get; set; }

    }
}