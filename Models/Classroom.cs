using System;
using System.Collections.Generic;

namespace RegistryWebApplication.Models
{
    public partial class Classroom
    {
        public Classroom()
        {
            Works = new HashSet<Work>();
        }

        public int ClassroomId { get; set; }
        public string Number { get; set; }

        public virtual ICollection<Work> Works { get; set; }
    }
}
