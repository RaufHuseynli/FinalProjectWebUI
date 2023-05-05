using FinalProjectWebUI.Models.DataContext;
using FinalProjectWebUI.Models.Entity;
using FinalProjectWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;

namespace FinalProjectWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class SizeColorController : Controller
    {
        readonly FinalDbContext db;
  
        public SizeColorController(FinalDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var vm = new ColorSizeViewModel();
            vm.Colors = db.Color.ToList();
            vm.Sizes = db.Sizes.ToList();
            vm.Genders = db.Genders.ToList();
            return View(vm);
        }

        #region Color
        public IActionResult AddColor()
        {
            return View();
        }

        [HttpPost]
        [Obsolete]
        public IActionResult AddColor(Color color)
        {
                db.Color.Add(color);
                db.SaveChanges();
                return RedirectToAction("index");

        }


        public IActionResult UpdateColor(int id)
        {
            
            var color = db.Color.Find(id);
  
            return View(color);
        }

        [HttpPost]
        [Obsolete]
        public IActionResult UpdateColor(Color color)
        {
                    db.Color.Update(color);
                    db.SaveChanges();
                    return RedirectToAction("index");

        }



        [Obsolete]
        public IActionResult DeleteColor(int id)
        {
            var color = db.Color.Find(id);
            db.Color.Remove(color);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        #endregion

        #region Size
        public IActionResult AddSize()
        {
            return View();
        }

        [HttpPost]
        [Obsolete]
        public IActionResult AddSize(Size size)
        {
            db.Sizes.Add(size);
            db.SaveChanges();
            return RedirectToAction("index");

        }


        public IActionResult UpdateSize(int id)
        {

            var size = db.Sizes.Find(id);

            return View(size);
        }

        [HttpPost]
        [Obsolete]
        public IActionResult UpdateSize(Size size)
        {
                    db.Sizes.Update(size);
                    db.SaveChanges();
                    return RedirectToAction("index"); 

        }



        [Obsolete]
        public IActionResult DeleteSize(int id)
        {
            var size = db.Sizes.Find(id);
            db.Sizes.Remove(size);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        #endregion

        #region Gender

        public IActionResult GenderAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenderAdd(Genders gender)
        {


            db.Genders.Add(gender);
            db.SaveChanges();
            ModelState.Clear();
            return View();


        }


        public IActionResult GenderUpdate(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index");
            }
            else
            {
                var gender = db.Genders.Find(id);
                return View(gender);
            }
        }

        [HttpPost]
        public IActionResult GenderUpdate(Genders gender)
        {


            db.Genders.Update(gender);
            db.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("index");
        }


        public IActionResult GenderDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index");
            }
            else
            {
                var gender = db.Genders.Find(id);
                db.Genders.Remove(gender); db.SaveChanges();
                return RedirectToAction("index");
            }

        }

        #endregion
    }
}
