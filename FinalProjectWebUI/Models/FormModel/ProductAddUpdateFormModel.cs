using FinalProjectWebUI.Models.Entity;

namespace FinalProjectWebUI.Models.FormModel
{
    public class ProductAddUpdateFormModel
    {
        public Product Product { get; set; }
        public IEnumerable<ProductCollection> ProductCollections { get; set; }
    }
}
