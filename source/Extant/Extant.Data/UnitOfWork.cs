//-----------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using NHibernate;

namespace Extant.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ISession CurrentSession { get; }
        void Commit();
        void Rollback();
    }

    /// <summary>
    /// See http://trason.net/journal/2009/10/7/bootstrapping-nhibernate-with-structuremap.html
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly ITransaction _transaction;

        public UnitOfWork(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            CurrentSession = _sessionFactory.OpenSession();
            _transaction = CurrentSession.BeginTransaction();
        }

        public ISession CurrentSession { get; private set; }

        public void Dispose()
        {
            if (CurrentSession != null)
            {
                CurrentSession.Close();
                CurrentSession = null;
            }
        }

        public void Commit()
        {
            if (_transaction.IsActive)
                _transaction.Commit();
        }

        public void Rollback()
        {
            if (_transaction.IsActive)
                _transaction.Rollback();
        }
    }
}