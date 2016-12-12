//-----------------------------------------------------------------------
// <copyright file="ExtantRoleProvider.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Configuration.Provider;
using System.Linq;
using System.Web.Security;
using Extant.Data.Entities;
using Extant.Data.Repositories;
using StructureMap;

namespace Extant.Web.Infrastructure
{
    public class ExtantRoleProvider : RoleProvider
    {
        private string applicationName;

        protected IUserRepository UserRepo
        {
            get { return (IUserRepository) System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserRepository)); }
        }

        protected IRoleRepository RoleRepo
        {
            get { return (IRoleRepository) System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleRepository)); }
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            applicationName = MembershipHelper.GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);

            base.Initialize(name, config);
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var user = UserRepo.GetByEmail(username);
            if (null == user) throw new ProviderException(string.Format("User '{0}' does not exist", username));
            return user.Roles.Any(r => string.Equals(r.RoleName, roleName));
        }

        public override string[] GetRolesForUser(string username)
        {
            var user = UserRepo.GetByEmail(username);
            if (null == user) throw new ProviderException(string.Format("User '{0}' does not exist", username));
            return user.Roles.Select(r => r.RoleName).ToArray();
        }

        public override void CreateRole(string roleName)
        {
            var roleRepo = RoleRepo;
            var role = roleRepo.GetByName(roleName);
            if (null != role) throw new ProviderException(string.Format("Role '{0}' already exists", roleName));
            var newRole = new Role {RoleName = roleName};
            roleRepo.Save(newRole);
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            var roleRepo = RoleRepo;
            var userRepo = UserRepo;
            var role = roleRepo.GetByName(roleName);
            if (null == role) 
                throw new ProviderException(string.Format("Role '{0}' does not exist", roleName));
            var users = userRepo.GetUsersInRole(roleName);
            if ( throwOnPopulatedRole && users.Any())
                throw new ProviderException("Cannot delete a populated role");
            foreach ( var user in users )
            {
                user.Roles.Remove(role);
                userRepo.Save(user);
            }
            roleRepo.Delete(role);
            return true;
        }

        public override bool RoleExists(string roleName)
        {
            return null != RoleRepo.GetByName(roleName);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            var userRepo = UserRepo;
            var roles = RoleRepo.GetAll().Where(r => roleNames.Contains(r.RoleName));
            foreach ( var username in usernames )
            {
                var user = userRepo.GetByEmail(username);
                if (null != user)
                {
                    foreach (var role in roles)
                    {
                        user.AddRole(role);
                    }
                    userRepo.Save(user);
                }
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            var userRepo = UserRepo;
            var roles = RoleRepo.GetAll().Where(r => roleNames.Contains(r.RoleName));
            foreach (var username in usernames )
            {
                var user = userRepo.GetByEmail(username);
                if (null != user)
                {
                    foreach (var role in roles)
                    {
                        user.Roles.Remove(role);
                    }
                    userRepo.Save(user);
                }
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return UserRepo.GetUsersInRole(roleName).Select(u => u.Email).ToArray();
        }

        public override string[] GetAllRoles()
        {
            return RoleRepo.GetAll().Select(r => r.RoleName).ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return UserRepo.GetUsersInRole(roleName, usernameToMatch).Select(u => u.Email).ToArray();
        }

        public override string ApplicationName
        {
            get { return applicationName; }
            set { applicationName = value; }
        }
    }
}