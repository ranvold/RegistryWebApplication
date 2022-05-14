using System;
using System.Collections.Generic;

namespace RegistryWebApplication.Models
{
    public partial class WorksDefense
    {
        public int WorkId { get; set; }
        public DateTime DefenseDate { get; set; }
        public int ClassroomId { get; set; }
        public int CommissionId { get; set; }

        public virtual Classroom Classroom { get; set; } = null!;
        public virtual Commission Commission { get; set; } = null!;
        public virtual Work Work { get; set; } = null!;
    }
}
