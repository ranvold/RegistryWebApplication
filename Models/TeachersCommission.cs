using System;
using System.Collections.Generic;

namespace RegistryWebApplication.Models
{
    public partial class TeachersCommission
    {
        public int TeacherId { get; set; }
        public int CommissionId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public virtual Commission Commission { get; set; } = null!;
        public virtual Teacher Teacher { get; set; } = null!;
    }
}
