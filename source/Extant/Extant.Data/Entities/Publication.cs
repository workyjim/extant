//-----------------------------------------------------------------------
// <copyright file="Publication.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using Extant.Data.Search;
using NHibernate.Search.Attributes;

namespace Extant.Data.Entities
{
    [Indexed]
    public class Publication : Entity
    {
        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string Title { get; set; }

        public virtual string Url { get; set; }

        public virtual int? Pmid { get; set; }

        public virtual string Journal { get; set; }

        public virtual string PublicationDate { get; set; }

        private IList<string> authors = new List<string>();
        [Field(Index.Tokenized, Store = Store.Yes)]
        [FieldBridge(typeof(ListStringFieldBridge))]
        public virtual IList<string> Authors
        {
            get { return authors; }
            set { authors = value; }
        }

        private IList<string> meshTerms = new List<string>();
        [Field(Index.Tokenized, Store = Store.Yes)]
        [FieldBridge(typeof(ListStringFieldBridge))]
        public virtual IList<string> MeshTerms
        {
            get { return meshTerms; }
            set { meshTerms = value; }
        }
    }
}