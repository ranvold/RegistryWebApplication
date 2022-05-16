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
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FathersName { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Work> Works { get; set; }
    }
}
