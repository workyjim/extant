//-----------------------------------------------------------------------
// <copyright file="DataItemRepository.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Extant.Data.Entities;

namespace Extant.Data.Repositories
{
    public interface IDataItemRepository : IRepository<DataItem>
    {
        IEnumerable<DataItem> Search(string term);
        void RebuildSearchIndex();
    }

    public class DataItemRepository : Repository<DataItem>, IDataItemRepository
    {
        public DataItemRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<DataItem> Search(string term)
        {
            var query = string.Join("* AND ", term.Split(' ').Where(w => w.Length > 0));
            var search = NHibernate.Search.Search.CreateFullTextSession(UnitOfWork.CurrentSession);
            return search.CreateFullTextQuery<DataItem>("DataItemName", string.Format("{0}*", query))
                         .List<DataItem>();
        }

        public void RebuildSearchIndex()
        {
            var dataitems = GetAll();
            var searchSession = NHibernate.Search.Search.CreateFullTextSession(UnitOfWork.CurrentSession);
            searchSession.PurgeAll(typeof(DataItem));
            foreach (var dataitem in dataitems)
            {
                searchSession.Index(dataitem);
            }
        }
    }
}