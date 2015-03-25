//-----------------------------------------------------------------------
// <copyright file="User.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Extant.Data.Entities
{
    public class User : Entity
    {
        public virtual string UserName { get; set; }

        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

        public virtual string Salt { get; set; }

        public virtual bool IsApproved { get; set; }

        public virtual bool IsLockedOut { get; set; }

        public virtual DateTime CreationDate { get; set; }

        public virtual DateTime? LastLoginDate { get; set; }

        public virtual DateTime? LastPasswordChangeDate { get; set; }

        public virtual DateTime? LastLockedOutDate { get; set; }

        public virtual DateTime? IncorrectPasswordWindowStart { get; set; }

        public virtual int IncorrectPasswordCount { get; set; }

        public virtual string PasswordResetCode { get; set; }

        public virtual DateTime? PasswordResetDate { get; set; }

        public virtual bool Deleted { get; set; }

        public virtual bool EmailValidated { get; set; }

        public virtual string EmailValidationCode { get; set; }

        private IList<DiseaseArea> diseaseAreas = new List<DiseaseArea>();
        public virtual IList<DiseaseArea> DiseaseAreas
        {
            get { return diseaseAreas; }
            set { diseaseAreas = value; }
        }

        private IList<Role> roles = new List<Role>();
        public virtual IList<Role> Roles
        {
            get { return roles; }
            set { roles = value; }
        }

        public virtual void AddRole(Role role)
        {
            if ( !roles.Contains(role) ) roles.Add(role);
        }
    }
}