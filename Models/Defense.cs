using System;
using System.Collections.Generic;

namespace RegistryWebApplication.Models
{
    public partial class Defense
    {
        public int Id { get; set; }
        public DateTime DefenseDate { get; set; }
        public bool HasItBeen { get; set; }
        public int ClassroomId { get; set; }
        public int CommissionId { get; set; }
        public int WorkId { get; set; }

        public virtual Classroom Classroom { get; set; } = null!;
        public virtual Commission Commission { get; set; } = null!;
        public virtual Work IdNavigation { get; set; } = null!;
    }
}
