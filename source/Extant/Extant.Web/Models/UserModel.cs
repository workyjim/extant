//-----------------------------------------------------------------------
// <copyright file="UserModel.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace Extant.Web.Models
{
    public class UserModel
    {
        [Required]
        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Required]
        [Email]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        public IEnumerable<int> DiseaseAreas { get; set; }

        public IEnumerable<DiseaseAreaBasicModel> AllDiseaseAreas { get; set; }
    }
}