//-----------------------------------------------------------------------
// <copyright file="PagedStudyListModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using NWeH.Paging;

namespace Extant.Web.Models
{
    public class PagedStudyListModel
    {
        public IPagedList<Extant.Web.Models.StudyBasicModel> Studies { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }
    }
}