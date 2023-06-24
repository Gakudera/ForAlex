using System;
using System.Collections.Generic;

namespace Guschin.GraduateProject.Entities
{
    public partial class Product
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Price { get; set; }
        public Guid ProductTypeId { get; set; }
        public string Logo { get; set; } = null!;

        public virtual ProductType ProductType { get; set; } = null!;
        public virtual Chat? Chat { get; set; }
    }
}
