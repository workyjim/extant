//-----------------------------------------------------------------------
// <copyright file="ExtantRoleProviderTests.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using Extant.Data.Entities;
using Extant.Data.Repositories;
using Extant.Web.Infrastructure;
using NUnit.Framework;
using Rhino.Mocks;
using StructureMap;

namespace Extant.Tests.Unit.Web.Infrastructure
{
    [TestFixture]
    public class ExtantRoleProviderTests
    {
        private IRoleRepository roleRepo;
        private IUserRepository userRepo;
        private ExtantRoleProvider roleProvider;

        [SetUp]
        public void SetUp()
        {
            roleRepo = MockRepository.GenerateMock<IRoleRepository>();
            ObjectFactory.Inject(roleRepo);
            userRepo = MockRepository.GenerateMock<IUserRepository>();
            ObjectFactory.Inject(userRepo);
            roleProvider = new ExtantRoleProvider();
        }

        [TearDown]
        public void TearDown()
        {
            ObjectFactory.EjectAllInstancesOf<IRoleRepository>();
            ObjectFactory.EjectAllInstancesOf<IUserRepository>();
            roleProvider = null;
        }

        [Test]
        public void IsUserInRole_Yes()
        {
            var role = new Role {Id = 1, RoleName = "Admin"};
            var user = new User {Id = 1, Email = "test@test.me", Roles = new List<Role> {role}};
            userRepo.Stub(ur => ur.GetByEmail("test@test.me")).Return(user);

            var result = roleProvider.IsUserInRole("test@test.me", "Admin");

            Assert.IsTrue(result);
        }

        [Test]
        public void IsUserInRole_No()
        {
            var role = new Role { Id = 1, RoleName = "Admin" };
            var user = new User { Id = 1, Email = "test@test.me", Roles = new List<Role> { role } };
            userRepo.Stub(ur => ur.GetByEmail("test@test.me")).Return(user);

            var result = roleProvider.IsUserInRole("test@test.me", "User");

            Assert.IsFalse(result);
        }

        [Test]
        [ExpectedException(typeof(ProviderException))]
        public void IsUserInRole_MissingUser()
        {
            userRepo.Stub(ur => ur.GetByEmail("test@test.me")).Return(null);

            roleProvider.IsUserInRole("test@test.me", "Admin");
        }

        [Test]
        public void GetRolesForUser()
        {
            var role = new Role { Id = 1, RoleName = "Admin" };
            var role2 = new Role { Id = 2, RoleName = "Coordinator" };
            var user = new User { Id = 1, Email = "test@test.me", Roles = new List<Role> { role, role2 } };
            userRepo.Stub(ur => ur.GetByEmail("test@test.me")).Return(user);

            var result = roleProvider.GetRolesForUser("test@test.me");

            Assert.AreEqual(2, result.Length);
            Assert.IsTrue(result.Contains("Admin"));
            Assert.IsTrue(result.Contains("Coordinator"));
        }

        [Test]
        [ExpectedException(typeof(ProviderException))]
        public void GetRolesForUser_MissingUser()
        {
            userRepo.Stub(ur => ur.GetByEmail("test@test.me")).Return(null);

            roleProvider.GetRolesForUser("test@test.me");
        }

        [Test]
        public void DeleteRole()
        {
            var role = new Role { Id = 1, RoleName = "Admin" };
            var role2 = new Role { Id = 2, RoleName = "Coordinator" };
            var user1 = new User {Id = 1, Email = "test1@test.me", Roles = new List<Role> {role, role2}};
            var user2 = new User { Id = 2, Email = "test2@test.me", Roles = new List<Role> { role } };
            roleRepo.Stub(rr => rr.GetByName("Admin")).Return(role);
            userRepo.Stub(ur => ur.GetUsersInRole("Admin")).Return(new List<User> {user1, user2});

            roleProvider.DeleteRole("Admin", false);

            Assert.AreEqual(1, user1.Roles.Count);
            Assert.AreEqual("Coordinator", user1.Roles[0].RoleName);
            Assert.AreEqual(0, user2.Roles.Count);
        }

        [Test]
        [ExpectedException(typeof(ProviderException))]
        public void DeleteRole_MissingRole()
        {
            roleRepo.Stub(rr => rr.GetByName("Admin")).Return(null);
            roleProvider.DeleteRole("Admin", false);
        }

        [Test]
        [ExpectedException(typeof(ProviderException))]
        public void DeleteRole_UsersInRole_ThrowOnPopulated()
        {
            var role = new Role { Id = 1, RoleName = "Admin" };
            var role2 = new Role { Id = 2, RoleName = "Coordinator" };
            var user1 = new User { Id = 1, Email = "test1@test.me", Roles = new List<Role> { role, role2 } };
            var user2 = new User { Id = 2, Email = "test2@test.me", Roles = new List<Role> { role } };
            roleRepo.Stub(rr => rr.GetByName("Admin")).Return(role);
            userRepo.Stub(ur => ur.GetUsersInRole("Admin")).Return(new List<User> { user1, user2 });

            roleProvider.DeleteRole("Admin", true);
        }

        [Test]
        public void DeleteRole_NoUsersInRole_ThrowOnPopulated()
        {
            var role = new Role { Id = 1, RoleName = "Admin" };
            roleRepo.Stub(rr => rr.GetByName("Admin")).Return(role);
            userRepo.Stub(ur => ur.GetUsersInRole("Admin")).Return(new List<User>());

            try
            {
                roleProvider.DeleteRole("Admin", true);
            }
            catch(Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void RoleExists_Yes()
        {
            var role = new Role { Id = 1, RoleName = "Admin" };
            roleRepo.Stub(rr => rr.GetByName("Admin")).Return(role);

            var result = roleProvider.RoleExists("Admin");

            Assert.IsTrue(result);
        }

        [Test]
        public void RoleExists_No()
        {
            roleRepo.Stub(rr => rr.GetByName("Admin")).Return(null);

            var result = roleProvider.RoleExists("Admin");

            Assert.IsFalse(result);
        }

        [Test]
        public void AddUsersToRoles()
        {
            var role1 = new Role {Id = 1, RoleName = "Role1"};
            var role2 = new Role { Id = 2, RoleName = "Role2" };
            var role3 = new Role { Id = 3, RoleName = "Role3" };
            var user1 = new User {Id = 1, Email = "test1@test.me", Roles = new List<Role>()};
            var user2 = new User { Id = 2, Email = "test2@test.me", Roles = new List<Role>{role1} };
            roleRepo.Stub(rr => rr.GetAll()).Return(new List<Role> { role1, role2, role3 });
            userRepo.Stub(ur => ur.GetByEmail("test1@test.me")).Return(user1);
            userRepo.Stub(ur => ur.GetByEmail("test2@test.me")).Return(user2);

            roleProvider.AddUsersToRoles(new[] { "test1@test.me", "test2@test.me" }, new[]{"Role1", "Role2"});

            Assert.AreEqual(2, user1.Roles.Count);
            Assert.IsTrue(user1.Roles.Contains(role1));
            Assert.IsTrue(user1.Roles.Contains(role2));
            Assert.AreEqual(2, user2.Roles.Count);
            Assert.IsTrue(user2.Roles.Contains(role1));
            Assert.IsTrue(user2.Roles.Contains(role2));
        }

        [Test]
        public void AddUsersToRoles_MissingUser()
        {
            var role1 = new Role { Id = 1, RoleName = "Role1" };
            var role2 = new Role { Id = 2, RoleName = "Role2" };
            var role3 = new Role { Id = 3, RoleName = "Role3" };
            var user1 = new User { Id = 1, Email = "test1@test.me", Roles = new List<Role>() };
            roleRepo.Stub(rr => rr.GetAll()).Return(new List<Role> { role1, role2, role3 });
            userRepo.Stub(ur => ur.GetByEmail("test1@test.me")).Return(user1);
            userRepo.Stub(ur => ur.GetByEmail("test2@test.me")).Return(null);

            roleProvider.AddUsersToRoles(new[] { "test1@test.me", "test2@test.me" }, new[] { "Role1", "Role2" });

            Assert.AreEqual(2, user1.Roles.Count);
            Assert.IsTrue(user1.Roles.Contains(role1));
            Assert.IsTrue(user1.Roles.Contains(role2));
        }

        [Test]
        public void RemoveUsersFromRoles()
        {
            var role1 = new Role { Id = 1, RoleName = "Role1" };
            var role2 = new Role { Id = 2, RoleName = "Role2" };
            var role3 = new Role { Id = 3, RoleName = "Role3" };
            var user1 = new User { Id = 1, Email = "test1@test.me", Roles = new List<Role>{ role1, role2 } };
            var user2 = new User { Id = 2, Email = "test2@test.me", Roles = new List<Role> { role1 } };
            roleRepo.Stub(rr => rr.GetAll()).Return(new List<Role> { role1, role2, role3 });
            userRepo.Stub(ur => ur.GetByEmail("test1@test.me")).Return(user1);
            userRepo.Stub(ur => ur.GetByEmail("test2@test.me")).Return(user2);

            roleProvider.RemoveUsersFromRoles(new[] { "test1@test.me", "test2@test.me" }, new[] { "Role2" });

            Assert.AreEqual(1, user1.Roles.Count);
            Assert.IsTrue(user1.Roles.Contains(role1));
            Assert.AreEqual(1, user2.Roles.Count);
            Assert.IsTrue(user2.Roles.Contains(role1));
        }

        [Test]
        public void RemoveUsersFromRoles_MissingUser()
        {
            var role1 = new Role { Id = 1, RoleName = "Role1" };
            var role2 = new Role { Id = 2, RoleName = "Role2" };
            var role3 = new Role { Id = 3, RoleName = "Role3" };
            var user1 = new User { Id = 1, Email = "test1@test.me", Roles = new List<Role>{ role1, role2 } };
            roleRepo.Stub(rr => rr.GetAll()).Return(new List<Role> { role1, role2, role3 });
            userRepo.Stub(ur => ur.GetByEmail("test1@test.me")).Return(user1);
            userRepo.Stub(ur => ur.GetByEmail("test2@test.me")).Return(null);

            roleProvider.RemoveUsersFromRoles(new[] { "test1@test.me", "test2@test.me" }, new[] { "Role2" });

            Assert.AreEqual(1, user1.Roles.Count);
            Assert.IsTrue(user1.Roles.Contains(role1));
        }

        [Test]
        public void GetUsersInRole()
        {
            var user1 = new User {Id = 1, Email = "test1@test.me"};
            var user2 = new User {Id = 2, Email = "test2@test.me"};
            userRepo.Stub(ur => ur.GetUsersInRole("Role1")).Return(new List<User> {user1, user2});

            var result = roleProvider.GetUsersInRole("Role1");

            Assert.AreEqual(2, result.Length);
            Assert.IsTrue(result.Contains("test1@test.me"));
            Assert.IsTrue(result.Contains("test2@test.me"));
        }

        [Test]
        public void GetAllRoles()
        {
            var role1 = new Role {Id = 1, RoleName = "Role1"};
            var role2 = new Role { Id = 2, RoleName = "Role2" };
            roleRepo.Stub(rr => rr.GetAll()).Return(new List<Role> {role1, role2});

            var result = roleProvider.GetAllRoles();

            Assert.AreEqual(2, result.Length);
            Assert.IsTrue(result.Contains("Role1"));
            Assert.IsTrue(result.Contains("Role2"));
        }

        [Test]
        public void FindUsersInRole()
        {
            var user1 = new User { Id = 1, Email = "test1@test.me" };
            var user2 = new User { Id = 2, Email = "test2@test.me" };
            userRepo.Stub(ur => ur.GetUsersInRole("Role1", "test")).Return(new List<User> { user1, user2 });

            var result = roleProvider.FindUsersInRole("Role1", "test");

            Assert.AreEqual(2, result.Length);
            Assert.IsTrue(result.Contains("test1@test.me"));
            Assert.IsTrue(result.Contains("test2@test.me"));
        }
    }
}