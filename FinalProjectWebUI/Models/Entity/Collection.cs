namespace FinalProjectWebUI.Models.Entity
{
    public class Collection:BaseEntity
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public ICollection<ProductCollection> Collections { get; set; }
    }
}
