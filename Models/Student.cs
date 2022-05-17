using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegistryWebApplication.Models
{
    public partial class Student
    {
        public Student()
        {
            Works = new HashSet<Work>();
        }

        public int StudentId { get; set; }

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
        public string Email { get; set; }

        public virtual ICollection<Work> Works { get; set; }
    }
}
