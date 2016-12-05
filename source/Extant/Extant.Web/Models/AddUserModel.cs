//-----------------------------------------------------------------------
// <copyright file="AddUserModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DataAnnotationsExtensions;

namespace Extant.Web.Models
{
    public class AddUserModel
    {
        [Required]
        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Required]
        [Email]
        public string Email { get; set; }

        public int[] DiseaseAreas { get; set; }

        [Display(Name = "Administrator")]
        public bool IsAdministrator { get; set; }

        [Display(Name = "Disease Area Lead")]
        public bool IsHubLead { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<DiseaseAreaBasicModel> AllDiseaseAreas { get; set; }

        public bool HasAdminRole { get; set; }
    }
}