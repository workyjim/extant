//-----------------------------------------------------------------------
// <copyright file="EditUserModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DataAnnotationsExtensions;
using Extant.Data.Entities;

namespace Extant.Web.Models
{
    public class EditUserModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Required]
        [Email]
        public string Email { get; set; }

        public IEnumerable<int> DiseaseAreas { get; set; }

        public bool IsApproved { get; set; }

        public bool IsLockedOut { get; set; }

        public bool EmailValidated { get; set; }

        public string LastLoginDate { get; set; }

        [Display(Name = "Administrator")]
        public bool IsAdministrator { get; set; }

        [Display(Name = "Disease Area Lead")]
        public bool IsHubLead { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<DiseaseAreaBasicModel> AllDiseaseAreas { get; set; }

        public bool HasAdminRole { get; set; }
    }
}