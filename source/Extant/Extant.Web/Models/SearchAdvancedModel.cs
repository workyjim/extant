//-----------------------------------------------------------------------
// <copyright file="SearchAdvancedModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Extant.Web.Models
{
    public class SearchAdvancedModel
    {
        public IEnumerable<SearchLineModel> SearchLines { get; set; }
    }

    public class SearchLineModel
    {
        public int SearchOperator { get; set; }
        public int SearchField { get; set; }
        public string SearchTerm { get; set; }
        public bool IsPhrase { get; set; }
    }

}