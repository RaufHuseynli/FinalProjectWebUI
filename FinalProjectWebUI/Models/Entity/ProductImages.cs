namespace FinalProjectWebUI.Models.Entity
{
    public class ProductImages:BaseEntity
    {
        public string ImagePath { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsMain { get; set; }
    }
}
