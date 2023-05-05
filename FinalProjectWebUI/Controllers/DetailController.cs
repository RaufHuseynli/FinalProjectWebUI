using FinalProjectWebUI.Models.DataContext;
using FinalProjectWebUI.Models.Entity;
using FinalProjectWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinalProjectWebUI.Controllers
{
    public class DetailController : Controller
    {
        readonly FinalDbContext db;
        public DetailController(FinalDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index(int id, int categorystore)
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
            var vm = new DetailViewModel();
            vm.Product = db.Products.Include(p=>p.ProductImages).FirstOrDefault(x => x.Id == id);
            vm.ProductColorSizeCount = db.ProductColorSizeCount.Include(p=>p.Size).Include(p=> p.Color).Where(p=>p.ProductId== id).ToList();
            return View(vm);
        }


    }
}
