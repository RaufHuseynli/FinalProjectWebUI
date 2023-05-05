using FinalProjectWebUI.Models.Entity;

namespace FinalProjectWebUI.Models.ViewModel
{
    public class ProductViewModel
    {
        public IEnumerable<dynamic> Products { get; set; }
        public dynamic Product { get; set; }
        public IEnumerable<ProductColorSizeCount> ProductColorSizeCounts{ get; set; }

        public Basket? Basket { get; set; }
    }
}
