//-----------------------------------------------------------------------
// <copyright file="PubmedResult.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Extant.Pubmed
{
    public class PubmedResult
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<string> Authors { get; set; }
        public string Journal { get; set; }
        public string PublicationDate { get; set; }
    }
}