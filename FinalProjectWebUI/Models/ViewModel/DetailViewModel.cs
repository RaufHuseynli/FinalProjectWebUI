using FinalProjectWebUI.Models.Entity;

namespace FinalProjectWebUI.Models.ViewModel
{
    public class DetailViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<ProductColorSizeCount> ProductColorSizeCount { get; set; }
    }
}
