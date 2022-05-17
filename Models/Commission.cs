using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegistryWebApplication.Models
{
    public partial class Commission
    {
        public Commission()
        {
            TeachersCommissions = new HashSet<TeachersCommission>();
            Works = new HashSet<Work>();
        }

        public int CommissionId { get; set; }

        [Required]
        [Display(Name = "Head last name")]
        public string HeadLastName { get; set; }

        [Required]
        [Display(Name = "Head first name")]
        public string HeadFirstName { get; set; }

        [Required]
        [Display(Name = "Head fathers name")]
        public string HeadFathersName { get; set; }

        public virtual ICollection<TeachersCommission> TeachersCommissions { get; set; }
        public virtual ICollection<Work> Works { get; set; }
    }
}
