//-----------------------------------------------------------------------
// <copyright file="Repository.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using NHibernate.Criterion;

namespace Extant.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);

        IEnumerable<T> Get(int[] ids);

        IEnumerable<T> GetAll();

        IEnumerable<T> Find(string property, object value);

        T Save(T entity);

        T Update(T entity);

        void Delete(T entity);
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IUnitOfWork UnitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual T Get(int id)
        {
            return UnitOfWork.CurrentSession.Get<T>(id);
        }

        public virtual IEnumerable<T> Get(int[] ids)
        {
            return UnitOfWork.CurrentSession.CreateCriteria<T>()
                                            .Add(Restrictions.In("Id", ids))
                                            .List<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return UnitOfWork.CurrentSession.CreateCriteria<T>().List<T>();
        }

        public IEnumerable<T> Find(string property, object value)
        {
            return UnitOfWork.CurrentSession.CreateCriteria<T>()
                                            .Add(Restrictions.Eq(property, value))
                                            .List<T>();
        }

        public virtual T Save(T entity)
        {
            UnitOfWork.CurrentSession.SaveOrUpdate(entity);
            return entity;
        }

        public virtual T Update(T entity)
        {
            var merged = UnitOfWork.CurrentSession.Merge(entity);
            UnitOfWork.CurrentSession.SaveOrUpdate(merged);
            return (T)merged;
        }

        public virtual void Delete(T entity)
        {
            UnitOfWork.CurrentSession.Delete(entity);
        }
    }
}