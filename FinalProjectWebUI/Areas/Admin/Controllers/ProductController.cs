using FinalProjectWebUI.Models.Entity;
using FinalProjectWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Net.Mail;
using System.Net;
using FinalProjectWebUI.Models.DataContext;
using Microsoft.EntityFrameworkCore;
using FinalProjectWebUI.Helper;

namespace FinalProjectWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class ProductController : Controller
    {
        readonly FinalDbContext db;
        [Obsolete]
        Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        public ProductController(FinalDbContext db, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public IActionResult Index(int pageIndex = 1, int pageSize = 10)
        {

            var products = db.Products.Where(p => p.DeletedDate == null);
            var pagedModel = new PagedViewModel<Product>(products, pageIndex, pageSize, null);
            return View(pagedModel);
        }

        public IActionResult Add()
        {
           
            ViewBag.Gender = new SelectList(db.Genders.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            ViewBag.Category = new SelectList(db.Categories.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            ViewBag.Collection = new SelectList(db.Collections.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [Obsolete]
        public IActionResult Add(Product product, List<int> Collection)
        {

            ViewBag.Gender = new SelectList(db.Genders.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            ViewBag.Category = new SelectList(db.Categories.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            ViewBag.Collection = new SelectList(db.Collections.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            try
            {
                product.ProductImages = new List<ProductImages>();
                foreach (var item in product.Files)
                {

                    product.ProductImages.Add(new ProductImages
                    {
                        IsMain = item.IsMain,
                        ImagePath = Image.Add(item.File, env)

                    });
                }

                foreach (var item in product.ProductImages)
                {
                    if (item.IsMain == true)
                    {
                        product.ImagePath = item.ImagePath;
                    }
                }

                product.NewArrival = true;


                db.Products.Add(product);
                db.SaveChanges();

                foreach (var item in Collection)
                {
                    var productcollection = new ProductCollection();
                    productcollection.ProductId = product.Id;
                    productcollection.CollectionId = item;
                    var collect = db.ProductCollections.FirstOrDefault(p => p.ProductId == product.Id && p.CollectionId == item);
                    if (collect == null)
                    {
                        db.ProductCollections.Add(productcollection);
                        db.SaveChanges();

                    }

                }

                ViewBag.Error = "Product successfully added";
                ModelState.Clear();

                return View();
            }
            catch (Exception)
            {
                ViewBag.Error = "Please enter the information completely";
               
                ViewBag.Gender = new SelectList(db.Genders.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
              
                return View(product);
                throw;
            }

        }

        public IActionResult Update(int id)
        {
            var product = db.Products.Include(p => p.ProductImages).FirstOrDefault(p => p.Id == id);
            var sizcolor = db.ProductColorSizeCount.Include(p => p.Size).Include(p => p.Color).Include(p => p.Product).Where(p => p.ProductId == id).ToList();

            ViewBag.Gender = new SelectList(db.Genders.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            ViewBag.Category = new SelectList(db.Categories.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            ViewBag.Collection = new SelectList(db.Collections.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            if (product != null)
            {
                return View(product);
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        public IActionResult Detail(int id)
        {
            var product = db.Products.Include(p => p.ProductImages).Include(p => p.ProductColorSizeCount).FirstOrDefault(p => p.Id == id);
            //var sizcolor = db.ProductColorSizeCount.Include(p => p.Size).Include(p => p.Color).Include(p => p.Product).Where(p => p.ProductId == id).ToList();
            ViewBag.Gender = new SelectList(db.Genders.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            ViewBag.Category = new SelectList(db.Categories.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");

            if (product != null)
            {
              return View(product);
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [Obsolete]
        public IActionResult Update(Product product, List<int> Collection)
        {

            ViewBag.Gender = new SelectList(db.Genders.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            ViewBag.Category = new SelectList(db.Categories.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            ViewBag.Collection = new SelectList(db.Collections.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");


            product.ProductImages = new List<ProductImages>();



            var images = db.ProductImages.Where(pi => pi.ProductId == product.Id).ToList();
            foreach (var item in images)
            {
                if (product.Files.Any(f => f.File == null && string.IsNullOrWhiteSpace(f.TempPath) && f.Id == item.Id))
                {
                    db.ProductImages.Remove(item);
                    Image.Delete(item.ImagePath, env);

                }
                else if (product.Files.Any(f => f.Id == item.Id && f.IsMain))
                {
                    item.IsMain = true;

                }

                else
                {
                    item.IsMain = false;
                }

            }


            foreach (var item in product.Files.Where(f => f.File != null))
            {


                product.ProductImages.Add(new ProductImages
                {
                    IsMain = item.IsMain,
                    ImagePath = Image.Add(item.File, env)
                });


            }

            foreach (var image in product.ProductImages)
            {
                if (image.IsMain == true)
                {
                    product.ImagePath = image.ImagePath;
                }
            }

            if (product.ProductImages.Count == 0)
            {
                foreach (var item in product.Files)
                {
                    if (item.IsMain == true)
                    {
                        product.ImagePath = item.TempPath.ToString();
                    }
                }
            }

            foreach (var item in Collection)
            {
                var productcollection = new ProductCollection();
                productcollection.ProductId = product.Id;
                productcollection.CollectionId = item;

                var collect = db.ProductCollections.FirstOrDefault(p=> p.ProductId == product.Id && p.CollectionId == item);
                if(collect == null)
                {
                    db.ProductCollections.Add(productcollection);
                    db.SaveChanges();

                }

            }

            product.UpdatedDate = DateTime.Now;
            db.Update(product);
            db.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult AddProductDetail(int id)
        {
            ViewBag.Color = new SelectList(db.Color.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            ViewBag.Size = new SelectList(db.Sizes.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");

            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public IActionResult AddProductDetail(ProductColorSizeCount colorSizeCount)
        {
            var product = db.Products.FirstOrDefault(p => p.Id == colorSizeCount.ProductId);


            ViewBag.Color = new SelectList(db.Color.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            ViewBag.Size = new SelectList(db.Sizes.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");

            ViewBag.Id = product.Id;

            if (product.Count == null)
            {
                product.Count = 0;
            }


            product.Count = product.Count + colorSizeCount.Count;

            if (colorSizeCount.SizeId == null || colorSizeCount.ColorId == null || colorSizeCount.Count == 0 || colorSizeCount.Count == null)
            {
                ViewBag.Error = "Please enter the information completely";
                return View(colorSizeCount);
            }
            var sizecolorcount = db.ProductColorSizeCount.FirstOrDefault(p => p.ColorId == colorSizeCount.ColorId && p.SizeId == colorSizeCount.SizeId && p.ProductId == colorSizeCount.ProductId);
            if (sizecolorcount == null)
            {
                db.ProductColorSizeCount.Add(colorSizeCount);
                db.Products.Update(product);
                db.SaveChanges();

                return RedirectToAction("update", new { id = product.Id });
            }
            else
            {
                ViewBag.Error = "The size and color you entered is available. To update the quantity, you can go to the product update section and find this size and color and update it";
                return View(colorSizeCount);
            }

        }

        public IActionResult UpdateProductDetail(int id)
        {
            var detail = db.ProductColorSizeCount.FirstOrDefault(p => p.Id == id);
            ViewBag.Color = new SelectList(db.Color.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            ViewBag.Size = new SelectList(db.Sizes.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");

            ViewBag.Id = id;
            return View(detail);
        }

        [HttpPost]
        public IActionResult UpdateProductDetail(int id, ProductColorSizeCount colorSizeCount, int counts)
        {
            var product = db.Products.FirstOrDefault(p => p.Id == colorSizeCount.ProductId);

            if (product.Count == null)
            {
                product.Count = 0;
            }
            product.Count = product.Count - counts;
            product.Count = product.Count + colorSizeCount.Count;
            ViewBag.Color = new SelectList(db.Color.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            ViewBag.Size = new SelectList(db.Sizes.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            db.Products.Update(product);
            db.ProductColorSizeCount.Update(colorSizeCount);
            db.SaveChanges();
            return RedirectToAction("update", new { id = colorSizeCount.ProductId });
        }


        public IActionResult DeleteProductDetail(int id)
        {
            var detail = db.ProductColorSizeCount.Find(id);

            var product = db.Products.FirstOrDefault(p => p.Id == detail.ProductId);

            product.Count = product.Count - detail.Count;
            db.ProductColorSizeCount.Remove(detail);
            db.Products.Update(product);
            db.SaveChanges();
            return RedirectToAction("update", new { id = product.Id });
        }


        public IActionResult Delete(int id)
        {

            var product = db.Products.FirstOrDefault(p => p.Id == id);
            //var productcolorsize = db.ProductColorSizeCount.FirstOrDefault(p => p.ProductId == id);
            //if (productcolorsize != null)
            //{
            //    db.ProductColorSizeCount.Remove(productcolorsize);
            //}

            product.Count = 0;
            product.DeletedDate = DateTime.Now;
            db.Products.Update(product);
            db.SaveChanges();
            return RedirectToAction("index");

        }
    }
}
