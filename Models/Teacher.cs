﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegistryWebApplication.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            TeachersCommissions = new HashSet<TeachersCommission>();
            Works = new HashSet<Work>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Fathers name")]
        public string FathersName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public virtual ICollection<TeachersCommission> TeachersCommissions { get; set; }
        public virtual ICollection<Work> Works { get; set; }
    }
}
