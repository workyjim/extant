//-----------------------------------------------------------------------
// <copyright file="RoleRepository.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using Extant.Data.Entities;

namespace Extant.Data.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role GetByName(string name);
    }

    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Role GetByName(string rolename)
        {
            return
            UnitOfWork.CurrentSession.CreateQuery("from Role r where r.RoleName=:rolename")
                                     .SetString("rolename", rolename)
                                     .UniqueResult<Role>();
        }
    }
}