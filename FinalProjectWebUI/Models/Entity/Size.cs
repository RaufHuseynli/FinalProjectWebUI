using System.Collections.Generic;

namespace FinalProjectWebUI.Models.Entity
{
    public class Size : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<ProductColorSizeCount> ProductColorCloths { get; set; }
    }
}
