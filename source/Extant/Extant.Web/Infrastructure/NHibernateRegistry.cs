//-----------------------------------------------------------------------
// <copyright file="NHibernateRegistry.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Configuration;
using Extant.Data;
using NHibernate;
using StructureMap;
using StructureMap.Web;

namespace Extant.Web.Infrastructure
{
    public class NHibernateRegistry : Registry
    {
        public NHibernateRegistry()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["InbankExtant"].ConnectionString;
            var luceneFolder = ConfigurationManager.AppSettings["Lucene.Net.lockdir"];

            For<ICurrentUser>()
                .Use<HttpUser>();

            For<ISessionFactory>()
                .Singleton()
                .Use(x => ExtantSessionFactory.GetSessionFactory(connectionString, luceneFolder));

            For<ISession>()
                .Use(x => x.GetInstance<ISessionFactory>().OpenSession());

            For<IUnitOfWork>()
                .Use<UnitOfWork>();
        }
    }
}