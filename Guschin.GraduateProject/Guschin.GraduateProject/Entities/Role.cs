using System;
using System.Collections.Generic;

namespace Guschin.GraduateProject.Entities
{
    public partial class Role
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string RoleName { get; set; } = null!;
    }
}
