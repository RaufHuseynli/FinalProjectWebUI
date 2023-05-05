using FinalProjectWebUI.Models.Entity;

namespace FinalProjectWebUI.Models.ViewModel
{
    public class ProductCategoryViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> Product { get; set; }
    }
}
