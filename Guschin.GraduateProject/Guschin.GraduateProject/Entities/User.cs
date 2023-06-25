using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Guschin.GraduateProject.Entities
{
    public partial class User
    {
        public User()
        {
            Messages = new HashSet<Message>();
        }

        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Email { get; set; } = null!;
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public bool PhoneNumberConfirmed { get; set; }

        public virtual Person? Person { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
