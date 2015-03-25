//-----------------------------------------------------------------------
// <copyright file="AdvancedSearch.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Extant.Data.Search
{
    public class AdvancedSearch
    {
        public IEnumerable<SearchLine> SearchLines { get; set; }
    }

    public class SearchLine
    {
        public SearchOperator SearchOperator { get; set; }
        public SearchField SearchField { get; set; }
        public string SearchTerm { get; set; }
        public bool IsPhrase { get; set; }
    }

    public enum SearchOperator
    {
        AND = 1,
        OR = 2,
        NOT = 3
    }

    public enum SearchField
    {
        Any = 0,
        StudyName = 1,
        Description = 2,
        ChiefInvestigator = 3,
        DiseaseArea = 4,
        StudyDesign = 5,
        StudyStatus = 6,
        Institution = 7,
        Funder = 8,
        Samples = 9,
        PublicationTitle = 10,
        PublicationAuthor = 11,
        PublicationMeshTerm = 12,
        DataField = 13
    }
}