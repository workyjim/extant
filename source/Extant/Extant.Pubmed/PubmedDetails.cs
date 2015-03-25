//-----------------------------------------------------------------------
// <copyright file="PubmedDetails.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Extant.Pubmed
{
    public class PubmedDetails
    {
        public string Journal { get; set; }

        public string PublicationDate { get; set; }

        public IList<string> Authors { get; set; }

        public IList<string> MeshTerms { get; set; }
    }
}