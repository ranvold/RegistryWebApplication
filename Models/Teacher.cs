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

        public int Id { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string FathersName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<TeachersCommission> TeachersCommissions { get; set; }
        public virtual ICollection<Work> Works { get; set; }
    }
}
