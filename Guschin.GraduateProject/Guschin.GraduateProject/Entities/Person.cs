using System;
using System.Collections.Generic;

namespace Guschin.GraduateProject.Entities
{
    public partial class Person
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;
        public Guid UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
