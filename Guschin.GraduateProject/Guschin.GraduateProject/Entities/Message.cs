using System;
using System.Collections.Generic;

namespace Guschin.GraduateProject.Entities
{
    public partial class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = null!;
        public DateTime Date { get; set; }
        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }

        public virtual Chat Chat { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
