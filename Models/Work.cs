using System;
using System.Collections.Generic;

namespace RegistryWebApplication.Models
{
    public partial class Work
    {
        public int WorkId { get; set; }
        public string Description { get; set; } = null!;
        public byte Mark100 { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }

        public virtual Student Student { get; set; } = null!;
        public virtual Teacher Teacher { get; set; } = null!;
        public virtual WorksDefense WorksDefense { get; set; } = null!;
    }
}
