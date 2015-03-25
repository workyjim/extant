//-----------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Extant.Data.Entities;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace Extant.Data.Repositories
{

    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetNotDeleted();
        User GetByEmail(string email);
        IEnumerable<User> GetUsersInRole(string role);
        IEnumerable<User> GetUsersInRole(string role, string user);
        bool CanDeleteStudy(int studyId, string email, string hubLeadRole);
        bool CanEditStudy(int studyId, string email, string hubLeadRole);
        bool CanEditUser(int userId, string email, string hubLeadRole);
        bool CheckPasswordReset(string email, string code);
        IEnumerable<User> FindUsers(string term);
        IEnumerable<User> GetUsersInDiseaseAreas(int[] daIds);
        IEnumerable<User> GetEmailsForRegistrationNotification(int daid);
        User GetUserByEmailValidationCode(string code);
    }

    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<User> GetNotDeleted()
        {
            return
            UnitOfWork.CurrentSession.CreateCriteria<User>()
                                     .Add(Restrictions.Eq("Deleted", false))
                                     .List<User>();
        }

        public User GetByEmail(string email)
        {
            return
            UnitOfWork.CurrentSession.CreateQuery("from User u where u.Email=:email")
                                     .SetString("email", email)
                                     .UniqueResult<User>();
        }

        public IEnumerable<User> GetUsersInRole(string role)
        {
            return
                UnitOfWork.CurrentSession.CreateCriteria<User>()
                                         .CreateCriteria("Roles")
                                         .Add(Restrictions.Eq("RoleName", role))
                                         .List<User>();
        }

        public IEnumerable<User> GetUsersInRole(string role, string user)
        {
            return
                UnitOfWork.CurrentSession.CreateCriteria<User>()
                                         .Add(Restrictions.Like("Email", user))
                                         .CreateCriteria("Roles")
                                         .Add(Restrictions.Eq("RoleName", role))
                                         .List<User>();
        }

        public bool CanDeleteStudy(int studyId, string email, string hubLeadRole)
        {
            var result = UnitOfWork.CurrentSession.CreateQuery(
                "select count(*) from Study s join s.DiseaseAreas da " +
                "where s.Id=:studyid and ( s.Owner.Email=:principal " +
                "or da.Id in (select diseasearea.Id from User u join u.Roles role join u.DiseaseAreas diseasearea where u.Email=:principal and role.RoleName=:hubLeadRole))")
                .SetString("principal", email)
                .SetString("hubLeadRole", hubLeadRole)
                .SetInt32("studyid", studyId)
                .UniqueResult();
            return Convert.ToInt32(result) > 0;
        }

        public bool CanEditStudy(int studyId, string email, string hubLeadRole)
        {
            var result = UnitOfWork.CurrentSession.CreateQuery(
                "select count(*) from Study s join s.DiseaseAreas da left join s.Editors e " +
                "where s.Id=:studyid and ( s.Owner.Email=:principal " +
                "or e.Email=:principal " +
                "or da.Id in (select diseasearea.Id from User u join u.Roles role join u.DiseaseAreas diseasearea where u.Email=:principal and role.RoleName=:hubLeadRole))")
                .SetString("principal", email)
                .SetString("hubLeadRole", hubLeadRole)
                .SetInt32("studyid", studyId)
                .UniqueResult();
            return Convert.ToInt32(result) > 0;
        }

        public bool CanEditUser(int userId, string email, string hubLeadRole)
        {
            var currentUser =
                UnitOfWork.CurrentSession.CreateCriteria<User>().Add(Restrictions.Eq("Email", email))
                                                                .CreateCriteria("Roles")
                                                                .Add(Restrictions.Eq("RoleName", hubLeadRole))
                                                                .UniqueResult<User>();
            var user =
                UnitOfWork.CurrentSession.CreateCriteria<User>().Add(Restrictions.Eq("Id", userId))
                                                                .UniqueResult<User>();
            if (null == currentUser || null == user) return false;

            return currentUser.DiseaseAreas.Intersect(user.DiseaseAreas).Count() > 0;
        }

        public bool CheckPasswordReset(string email, string code)
        {
            var count = UnitOfWork.CurrentSession.CreateCriteria<User>()
                                                 .Add(Restrictions.Eq("Email", email))
                                                 .Add(Restrictions.Eq("PasswordResetCode", code))
                                                 .Add(Restrictions.Ge("PasswordResetDate", DateTime.Now.AddMinutes(-30.0)))
                                                 .SetProjection(Projections.RowCount())
                                                 .UniqueResult<int>();
            return 1 == count;
        }

        public User GetUserByEmailValidationCode(string code)
        {
            // Linq2NHibernate query - typesafe!
            var user = UnitOfWork.CurrentSession.Query<User>()
                                                    .Where(u => u.EmailValidationCode == code && !u.Deleted)
                                                    .SingleOrDefault();
            return user;
        }

        public IEnumerable<User> FindUsers(string term)
        {
            return UnitOfWork.CurrentSession.CreateCriteria<User>()
                                            .Add(Restrictions.Eq("Deleted", false))
                                            .Add(Restrictions.Like("UserName", string.Format("%{0}%", term)))
                                            .List<User>();
        }

        public IEnumerable<User> GetUsersInDiseaseAreas(int[] daIds)
        {
            return
                UnitOfWork.CurrentSession.CreateCriteria<User>()
                                         .Add(Restrictions.Eq("Deleted", false))
                                         .CreateCriteria("DiseaseAreas")
                                         .Add(Restrictions.In("Id", daIds))
                                         .List<User>();
        }

        public IEnumerable<User> GetEmailsForRegistrationNotification(int daid)
        {
            var result =
                UnitOfWork.CurrentSession.CreateQuery(
                "select distinct u from User u join u.DiseaseAreas da join u.Roles role " +
                "where role.RoleName=:hubLeadRole and da.Id=:daid"
                )
                .SetString("hubLeadRole", Constants.HubLeadRole)
                .SetInt32("daid", daid)
                .List<User>();
            return result;
        }
    }
}