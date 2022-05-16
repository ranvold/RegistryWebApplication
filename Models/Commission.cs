using System;
using System.Collections.Generic;

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
        public string HeadLastName { get; set; }
        public string HeadFirstName { get; set; }
        public string HeadFathersName { get; set; }

        public virtual ICollection<TeachersCommission> TeachersCommissions { get; set; }
        public virtual ICollection<Work> Works { get; set; }
    }
}
