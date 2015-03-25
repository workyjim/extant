//-----------------------------------------------------------------------
// <copyright file="AdminUserModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace Extant.Web.Models
{
    public class AdminUserModel
    {
        public virtual int Id { get; set; }

        public virtual string UserName { get; set; }

        public virtual string Email { get; set; }

        public virtual bool IsApproved { get; set; }

        public virtual bool IsLockedOut { get; set; }

        public virtual string LastLoginDate { get; set; }

        public virtual bool EmailValidated { get; set; }
    }
}