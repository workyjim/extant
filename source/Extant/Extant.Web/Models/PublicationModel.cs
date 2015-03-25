//-----------------------------------------------------------------------
// <copyright file="PublicationModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Extant.Web.Models
{
    public class PublicationModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Pmid { get; set; }
        public string Journal { get; set; }
        public string PublicationDate { get; set; }
        public IList<string> Authors { get; set; }
        public IList<string> MeshTerms { get; set; }
    }
}