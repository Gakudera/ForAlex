using System;
using System.Collections.Generic;

namespace Guschin.GraduateProject.Entities
{
    public partial class ProductType
    {
        public ProductType()
        {
            Products = new HashSet<Product>();
        }

        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
