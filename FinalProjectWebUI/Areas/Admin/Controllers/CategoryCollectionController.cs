using FinalProjectWebUI.Helper;
using FinalProjectWebUI.Models.DataContext;
using FinalProjectWebUI.Models.Entity;
using FinalProjectWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FinalProjectWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class CategoryCollectionController : Controller
    {
        readonly FinalDbContext db;
        [Obsolete]
        Microsoft.AspNetCore.Hosting.IHostingEnvironment env;

        [Obsolete]
        public CategoryCollectionController(FinalDbContext db, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public IActionResult Index()
        {
            var vm = new CategoryCollectionViewModel();

            vm.Categories= db.Categories.ToList();
            vm.Collections = db.Collections.ToList();
            return View(vm);
        }



        #region Category
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("index");

        }


        public IActionResult UpdateCategory(int id)
        {

            var category = db.Categories.Find(id);

            return View(category);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            db.Categories.Update(category);
            db.SaveChanges();
            return RedirectToAction("index");

        }



        public IActionResult DeleteCategory(int id)
        {
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        #endregion

        #region Collection
        public IActionResult AddCollection()
        {
            return View();
        }

        [HttpPost]
        [Obsolete]
        public IActionResult AddCollection(IFormFile file ,Collection collection)
        {
            if(file == null)
            {
                return View();
            }
            else
            {
                collection.ImagePath = Image.Add(file, env);
            }
            db.Collections.Add(collection);
            db.SaveChanges();
            return RedirectToAction("index");

        }


        public IActionResult UpdateCollection(int id)
        {

            var collection = db.Collections.Find(id);

            return View(collection);
        }

        [HttpPost]
        [Obsolete]
        public IActionResult UpdateCollection(IFormFile file, Collection collection)
        {
            if(file != null) {
                Image.Delete(collection.ImagePath, env);
                collection.ImagePath = Image.Add(file, env);
            }
            db.Collections.Update(collection);
            db.SaveChanges();
            return RedirectToAction("index");

        }

        public IActionResult CollectionDetail(int id)
        {
            var vm = new ProductCollectionViewModel();
            vm.Collection = db.Collections.FirstOrDefault(p => p.Id == id);
            vm.Products = db.ProductCollections.Include(p => p.Product).Where(p => p.CollectionId == id).ToList();
            return View(vm);
        }

        public IActionResult RemoveProductOnCollection(int id, int product)
        {
            var collect = db.ProductCollections.FirstOrDefault(p=>p.CollectionId == id && p.ProductId == product);
            if (collect != null)
            {
                db.ProductCollections.Remove(collect);

                db.SaveChanges();
                return RedirectToAction("CollectionDetail", new { id = id });
            }

            else
            {

                return RedirectToAction("index");
            }
        }

        [Obsolete]
        public IActionResult DeleteCollection(int id)
        {
            var collection = db.Collections.Find(id);
            Image.Delete(collection.ImagePath, env);
            db.Collections.Remove(collection);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        #endregion
    }
}
