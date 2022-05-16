using System;
using System.Collections.Generic;

namespace RegistryWebApplication.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            TeachersCommissions = new HashSet<TeachersCommission>();
            Works = new HashSet<Work>();
        }

        public int TeacherId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FathersName { get; set; }
        public string Email { get; set; }

        public virtual ICollection<TeachersCommission> TeachersCommissions { get; set; }
        public virtual ICollection<Work> Works { get; set; }
    }
}
