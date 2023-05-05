namespace FinalProjectWebUI.Models.Entity
{
    public class ProductStore:BaseEntity
    {
        public int ProductId { get; set; }
        public Product  Product { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }
    }
}
