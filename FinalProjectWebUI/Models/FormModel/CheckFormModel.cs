﻿using FinalProjectWebUI.Models.Entity;

namespace FinalProjectWebUI.Models.FormModel
{
    public class CheckFormModel
    {
        public List<FinalProjectWebUI.Models.Entity.Basket> Baskets { get; set; }
        public IEnumerable<Product> Products { get; set; }      
        public Orders Orders { get; set; }
        public Order Order { get; set; }
        public List<int> ProductId { get; set; }
        public string Password { get; set; }
        public List<Product> Productss { get; set; }

    }
}
