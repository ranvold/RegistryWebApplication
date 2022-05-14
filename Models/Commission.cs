using System;
using System.Collections.Generic;

namespace RegistryWebApplication.Models
{
    public partial class Commission
    {
        public Commission()
        {
            TeachersCommissions = new HashSet<TeachersCommission>();
            WorksDefenses = new HashSet<WorksDefense>();
        }

        public int CommissionId { get; set; }
        public int CommissionCode { get; set; }

        public virtual ICollection<TeachersCommission> TeachersCommissions { get; set; }
        public virtual ICollection<WorksDefense> WorksDefenses { get; set; }
    }
}
