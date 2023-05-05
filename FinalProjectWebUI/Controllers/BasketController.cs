using FinalProjectWebUI.Models.DataContext;
using FinalProjectWebUI.Models.Entity;
using FinalProjectWebUI.Models.FormModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

namespace FinalProjectWebUI.Controllers
{
    public class BasketController : Controller
    {

        readonly FinalDbContext db;


        public BasketController(FinalDbContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index(string? id,  int categorystore)
        {

            ViewBag.Categorystore = categorystore;
            var vm = new CheckFormModel();
            var products = new List<Product>();
            var product = new Product();

            string cookieValueFromReqq = Request.Cookies["FinalProduct"];
            var values = cookieValueFromReqq;


            if (values == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userr = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString();
                    vm.Baskets = db.Baskets.Where(p => p.UserId == userr).ToList();
                    if (vm.Baskets.Count() > 0)
                    {
                        foreach (var item in vm.Baskets)
                        {
                            product = await db.Products.Include(p => p.ProductColorSizeCount).FirstOrDefaultAsync(p => p.Id == item.ProductId);
                            products.Add(product);
                        }
                    }

                }

            }
            else
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userr = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString();
                    vm.Baskets = db.Baskets.Where(p => p.UserId == userr).ToList();
                    if (vm.Baskets.Count() > 0)
                    {
                        foreach (var item in vm.Baskets)
                        {
                            product = await db.Products.Include(p => p.ProductColorSizeCount).FirstOrDefaultAsync(p => p.Id == item.ProductId);
                            products.Add(product);
                        }
                    }

                }
                vm.Baskets = db.Baskets.Where(p => p.UserId == values).ToList();
                if (vm.Baskets.Count() > 0)
                {
                    foreach (var item in vm.Baskets)
                    {
                        product = await db.Products.Include(p => p.ProductColorSizeCount).FirstOrDefaultAsync(p => p.Id == item.ProductId);
                        products.Add(product);
                    }
                }
            }

            vm.Products = products;
            return View(vm);

        }

        [HttpPost]
        public IActionResult Index(CheckFormModel fm)
        {
            List <Product> products = new List<Product>();
            db.Order.Add(fm.Order);
            db.SaveChanges();
            foreach (var item in fm.ProductId)
            {
                var product = db.Products.FirstOrDefault(p => p.Id == item);

                products.Add(product);
            }

            foreach (var item in products)
            {
                var bs = db.Baskets.FirstOrDefault(p => p.ProductId == item.Id);
                db.Baskets.Remove(bs);
                Orders orders = new Orders();
                orders.OrderId = fm.Order.Id;
                orders.ProductId = item.Id;
                orders.OrderNumber = Guid.NewGuid().ToString();
                orders.Total = (decimal)(item.Price - ((item.Price * item.Discount) / 100));
                orders.Price= Convert.ToInt32(item.Price);
                orders.DisCountPrice= Convert.ToInt32(item.Discount);
                orders.approved = false;
                if (User.Identity.IsAuthenticated)
                {
                    var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString();
                    orders.UserId = user;
                }
                else
                {
                    orders.UserId = Guid.NewGuid().ToString();
                }
                db.Orders.Add(orders);

                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
