using FinalProjectWebUI.Models.Entity;

namespace FinalProjectWebUI.Models.ViewModel
{
    public class ProductCollectionViewModel
    {
        public Collection Collection { get; set; }
        public IEnumerable<ProductCollection> Products { get; set; }
    }
}
