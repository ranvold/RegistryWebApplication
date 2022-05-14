using System;
using System.Collections.Generic;

namespace RegistryWebApplication.Models
{
    public partial class Student
    {
        public Student()
        {
            Works = new HashSet<Work>();
        }

        public int StudentId { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string FathersName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Work> Works { get; set; }
    }
}
