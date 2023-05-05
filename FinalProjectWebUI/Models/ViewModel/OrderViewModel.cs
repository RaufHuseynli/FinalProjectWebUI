using FinalProjectWebUI.Models.Entity;

namespace FinalProjectWebUI.Models.ViewModel
{
    public class OrderViewModel
    {
        public IEnumerable<FinalProjectWebUI.Models.Entity.Basket> Baskets { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<FinalProjectWebUI.Models.Entity.Color> Color { get; set; }
        public FinalProjectWebUI.Models.Entity.Orders Orders { get; set; }

        public List<int> ProductId { get; set; }
        public string Password { get; set; }

        public Order Order { get; set; }
    }
}
