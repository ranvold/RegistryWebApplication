using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegistryWebApplication.Models
{
    public partial class Work
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public byte? Mark { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public int CommissionId { get; set; }

        [Required]
        public int ClassroomId { get; set; }

        public virtual Classroom Classroom { get; set; }
        public virtual Commission Commission { get; set; }
        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
