using FinalProjectWebUI.Models;
using FinalProjectWebUI.Models.DataContext;
using FinalProjectWebUI.Models.Entity;
using FinalProjectWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace FinalProjectWebUI.Controllers
{
    public class HomeController : Controller
    {
        readonly FinalDbContext db;
        public HomeController(FinalDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index(int category, int categorystore)
        {
            string cookieValueFromReqq = Request.Cookies["FinalProduct"];
            var values = cookieValueFromReqq;
            string cookieValueFromReq = "";
            if (values == null)
            {
                var cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(2000);
                cookieOptions.Path = "/";
                var vauesss = Guid.NewGuid().ToString();
                Response.Cookies.Append("FinalProduct", $"{vauesss}", cookieOptions);
            }
            else
            {
                cookieValueFromReq = Request.Cookies["FinalProduct"];
            }


            var value = cookieValueFromReq;
            var count = 0;
            var order = new Orders();

            if (User.Identity.IsAuthenticated)
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString();
                count = count + db.Baskets.Where(p => p.UserId == user).ToList().Count();
                order = db.Orders.FirstOrDefault(p => p.UserId == user);
            }

            count = count + db.Baskets.Where(p => p.UserId == value.ToString()).ToList().Count();
            order = db.Orders.FirstOrDefault(p => p.UserId == value);





            var countt = 0;
            if (count > 0)
            {
                countt = count;
            }


            ViewBag.Count = countt;
            ViewBag.Order = null;
            if (order != null)
            {
                ViewBag.Order = order.UserId;
            }



            ViewBag.Category = category;
            ViewBag.Categorystore = categorystore;
            var siteinfo = db.SiteInfos.FirstOrDefault();

            if (categorystore > 0) {
                siteinfo = db.SiteInfos.FirstOrDefault(p => p.StoreId == categorystore);
            }
            return View(siteinfo);
        }


        public IActionResult AddToCart(int? id, ProductViewModel vm)
        {


            var basket = new Basket();

            string cookieValueFromReqq = Request.Cookies["FinalProduct"];
            var values = cookieValueFromReqq;
            string cookieValueFromReq = "";
            if (values == null)
            {
                var cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(2000);
                cookieOptions.Path = "/";
                var vauesss = Guid.NewGuid().ToString();
                Response.Cookies.Append("FinalProduct", $"{vauesss}", cookieOptions);
                string cookieValueFromReqqs = Request.Cookies["FinalProduct"];
                cookieValueFromReq = cookieValueFromReqqs;
            }
            else
            {
                cookieValueFromReq = Request.Cookies["FinalProduct"];
            }

            var userId = cookieValueFromReq;

            if (User.Identity.IsAuthenticated)
            {
                userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString();
            }

            var basketrow = db.Baskets.FirstOrDefault(p => p.ProductId == id && p.UserId == userId);
            if (vm.Basket != null)
            {
                basket = vm.Basket;
                var basketrows = db.Baskets.FirstOrDefault(p => p.ProductId == vm.Basket.ProductId && p.UserId == userId);
                if (basketrows == null)
                {
                    basket.ProductId = (int)id;
                    basket.UserId = userId;
                  
                    db.Baskets.Add(basket);
                    db.SaveChanges();


                    return RedirectToAction("index", "detail", new { id = id });
                }
            }
            else
            {
                if (basketrow != null)
                {

                    return RedirectToAction("index", "detail", new { id = id });
                }
                else
                {

                    basket.ProductId = (int)id;
                    basket.UserId = userId;


                    db.Baskets.Add(basket);
                    db.SaveChanges();
                    return RedirectToAction("index", "detail", new { id = id });
                }
            }

            return RedirectToAction("index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}