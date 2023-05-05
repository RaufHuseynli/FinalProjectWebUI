using FinalProjectWebUI.Models.DataContext;
using FinalProjectWebUI.Models.Entity;
using FinalProjectWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProjectWebUI.Controllers
{
    public class ShopController : Controller
    {

        readonly FinalDbContext db;
        public ShopController(FinalDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index(int category, int color, int gender, int size, int max, bool top = false, bool newarrival = false, FilterViewModel? vm = null, int categorystore = 1, int collection = 0)
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


            ViewBag.Categorystore = categorystore;
            ViewBag.Category = category;
            ViewBag.Gender = gender;
            ViewBag.Size = size;
            ViewBag.Max = max;
            ViewBag.Color = color;
            ViewBag.Newarrival = newarrival;
            ViewBag.Top = top;
            ViewBag.collection = collection;
            if (vm.FilterFormModel!= null)
            {
                ViewBag.Category = vm.FilterFormModel.CategoryId;
                ViewBag.Gender = vm.FilterFormModel.GenderId;
                ViewBag.Size = vm.FilterFormModel.SizeId;
                ViewBag.Max = vm.FilterFormModel.Max;
                ViewBag.Color = vm.FilterFormModel.ColorId;
            }
           
            return View();
        }
    }
}
