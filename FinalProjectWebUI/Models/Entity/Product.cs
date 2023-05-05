using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using FinalProjectWebUI.Models.FormModel;

namespace FinalProjectWebUI.Models.Entity
{
    public class Product:BaseEntity
    {

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

		[System.ComponentModel.DataAnnotations.Required]
		public int CategoryId { get; set; }
		public Category Category { get; set; }
        public string? ImagePath { get; set; }
        public int GenderId { get; set; }
        public Genders Gender { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public decimal Price { get; set; }
        public int? Discount { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Description { get; set; }
        public bool NewArrival { get; set; }
        public int? Count { get; set; }
        public bool Top { get; set; }

        public ICollection<ProductColorSizeCount> ProductColorSizeCount { get; set; }
        public ICollection<ProductImages>? ProductImages { get; set; }
        [NotMapped]
        public ImageItemFormModel[] Files { get; set; }
        public ICollection<ProductCollection> Collections { get; set; }
    }
}
