//-----------------------------------------------------------------------
// <copyright file="RepositoryRegistry.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Extant.Data.Entities;
using Extant.Data.Repositories;
using StructureMap.Configuration.DSL;

namespace Extant.Web.Infrastructure
{
    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {
            For<IStudyRepository>().Use<StudyRepository>();
            For<IUserRepository>().Use<UserRepository>();
            For<IDiseaseAreaRepository>().Use<DiseaseAreaRepository>();
            For<IRoleRepository>().Use<RoleRepository>();
            For<IRepository<FileUpload>>().Use<Repository<FileUpload>>();
            For<IDataItemRepository>().Use<DataItemRepository>();
            For<IRepository<Category>>().Use<Repository<Category>>();
            For<IRepository<CategoryDataItem>>().Use<Repository<CategoryDataItem>>();
            For<IRepository<Publication>>().Use<Repository<Publication>>();
        }
    }
}