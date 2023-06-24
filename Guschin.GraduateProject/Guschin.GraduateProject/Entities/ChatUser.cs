using System;
using System.Collections.Generic;

namespace Guschin.GraduateProject.Entities
{
    public partial class ChatUser
    {
        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }

        public virtual Chat Chat { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
