using System.Collections.Generic;

namespace FinalProjectWebUI.Models.Entity
{
    public class Color:BaseEntity
    {
        public string Name { get; set; }

        public string HexCode { get; set; }

        public virtual ICollection<ProductColorSizeCount> ProductColorCloths { get; set; }
    }
}
