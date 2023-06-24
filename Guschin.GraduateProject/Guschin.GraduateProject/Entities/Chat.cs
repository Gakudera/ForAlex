using System;
using System.Collections.Generic;

namespace Guschin.GraduateProject.Entities
{
    public partial class Chat
    {
        public Chat()
        {
            Messages = new HashSet<Message>();
        }

        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<Message> Messages { get; set; }
    }
}
