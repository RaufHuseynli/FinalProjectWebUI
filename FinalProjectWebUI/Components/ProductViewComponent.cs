using FinalProjectWebUI.Models.DataContext;
using FinalProjectWebUI.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebUI.Components
{
    public class ProductViewComponent : ViewComponent
    {
        readonly FinalDbContext db;
        public ProductViewComponent(FinalDbContext db)
        {
            this.db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync(bool all = false, bool newarrival = false, bool top = false, int category = 0, int gender = 0, int size = 0, int max = 0, int color = 0, bool discount = false, int collection = 0)
        {
 

            var product = db.Products.Include(p=> p.ProductColorSizeCount).Include(p => p.ProductImages).Include(p=> p.Category).Where(p=> p.Count > 0 && p.DeletedDate == null).ToList();
            var products = new List<Product>();

            if(collection > 0)
            {
                var collections = db.ProductCollections.FirstOrDefault(p=> p.CollectionId == collection);
                product = product.Where(p => p.Id == collections.ProductId).ToList();
            }
            var colorsizecount = db.ProductColorSizeCount.ToList();
            if(category > 0)
            {
                product = product.Where(p=>p.CategoryId == category).ToList();
            }
            if (gender > 0)
            {
                product = product.Where(p => p.GenderId == gender).ToList();
            }

            if (newarrival == true)
            {
                product = product.Where(p => p.NewArrival == true).ToList();
            }

            if (max > 0)
            {
                product = product.Where(p => (p.Price - ((p.Price * p.Discount)) / 100) <= max).ToList();
            }

            if (top == true && all == true)
            {
                product = product.Where(p => p.Top == true).ToList();
            }
            if (top == true && all == false)
            {
                product = product.Where(p => p.Top == true).Take(1).ToList();
            }

            if (discount == true && all == true)
            {
                product = product.Where(p => p.Discount > 0).Take(3).ToList();
                ViewBag.discount = "true";
            }
            if (size > 0)
            {
                colorsizecount = colorsizecount.Where(p => p.SizeId == size).ToList();
                foreach (var item in colorsizecount)
                {
                    product = product.Where(p => p.Id == item.ProductId).ToList();
                    
                }

            }
            if (color > 0)
            {
                colorsizecount = colorsizecount.Where(p => p.ColorId == color).ToList();
                foreach (var item in colorsizecount)
                {
                    product = product.Where(p => p.Id == item.ProductId).ToList();

                }

            }


            if (product.Count() == 0 || product == null)
            {
                ViewBag.Notfound = "The product you are looking for is currently unavailable";
            }
            if (colorsizecount.Count() == 0 || colorsizecount == null)
            {
                ViewBag.Notfound = "The product you are looking for is currently unavailable";
            }
            return View(product);

        }
    }
}
