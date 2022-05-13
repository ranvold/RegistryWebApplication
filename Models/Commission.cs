using System;
using System.Collections.Generic;

namespace RegistryWebApplication.Models
{
    public partial class Commission
    {
        public Commission()
        {
            Defenses = new HashSet<Defense>();
            TeachersCommissions = new HashSet<TeachersCommission>();
        }

        public int Id { get; set; }
        public int CommissionCode { get; set; }

        public virtual ICollection<Defense> Defenses { get; set; }
        public virtual ICollection<TeachersCommission> TeachersCommissions { get; set; }
    }
}
