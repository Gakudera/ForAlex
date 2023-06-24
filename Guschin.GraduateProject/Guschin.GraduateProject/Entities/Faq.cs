using System;
using System.Collections.Generic;

namespace Guschin.GraduateProject.Entities
{
    public partial class Faq
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
    }
}
