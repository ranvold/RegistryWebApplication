using System;
using System.Collections.Generic;

namespace RegistryWebApplication.Models
{
    public partial class Classroom
    {
        public Classroom()
        {
            WorksDefenses = new HashSet<WorksDefense>();
        }

        public int ClassroomId { get; set; }
        public string ClassroomNum { get; set; } = null!;

        public virtual ICollection<WorksDefense> WorksDefenses { get; set; }
    }
}
