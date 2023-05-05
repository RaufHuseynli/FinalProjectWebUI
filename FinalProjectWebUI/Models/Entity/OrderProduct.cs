using FinalProjectWebUI.Models.Entity;
using FinalProjectWebUI.Models.Entity.Identity;

namespace FinalProjectWebUI.Models.Entity
{
    public class OrderProduct:BaseEntity  
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string OrderId { get; set; }


    }
}
