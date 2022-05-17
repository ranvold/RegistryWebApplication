using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegistryWebApplication.Models
{
    public partial class TeachersCommission
    {
        public int TeacherId { get; set; }
        public int CommissionId { get; set; }

        [Required]
        [Display(Name = "Defense date")]
        public DateTime DefenseDate { get; set; }

        public virtual Commission Commission { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
