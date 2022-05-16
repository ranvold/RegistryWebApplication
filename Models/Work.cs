using System;
using System.Collections.Generic;

namespace RegistryWebApplication.Models
{
    public partial class Work
    {
        public int WorkId { get; set; }
        public string Name { get; set; }
        public byte? Mark { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public int CommissionId { get; set; }
        public int ClassroomId { get; set; }

        public virtual Classroom Classroom { get; set; }
        public virtual Commission Commission { get; set; }
        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
