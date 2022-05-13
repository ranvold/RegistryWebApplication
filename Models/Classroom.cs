using System;
using System.Collections.Generic;

namespace RegistryWebApplication.Models
{
    public partial class Classroom
    {
        public Classroom()
        {
            Defenses = new HashSet<Defense>();
        }

        public int Id { get; set; }
        public string ClassroomNum { get; set; } = null!;

        public virtual ICollection<Defense> Defenses { get; set; }
    }
}
