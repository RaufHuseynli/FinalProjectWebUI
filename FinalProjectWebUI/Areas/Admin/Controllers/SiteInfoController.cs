using FinalProjectWebUI.Helper;
using FinalProjectWebUI.Models;
using FinalProjectWebUI.Models.DataContext;
using FinalProjectWebUI.Models.Entity;
using FinalProjectWebUI.Models.FormModel;
using FinalProjectWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FinalProjectWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class SiteInfoController : Controller
    {
        readonly FinalDbContext db;
        [Obsolete]
        Microsoft.AspNetCore.Hosting.IHostingEnvironment env;

        [Obsolete]
        public SiteInfoController(FinalDbContext db, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.db = db;
            this.env = env;
        }


        public IActionResult Index()
        {
            var vm = new SiteInfoViewModel();
            vm.SiteInfo = db.SiteInfos.ToList();
            vm.Stores = db.Stores.ToList();
            return View(vm);
        }

        #region Store

        public IActionResult StoreAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StoreAdd(Store store)
        {
            if(ModelState.IsValid)
            {
                db.Stores.Add(store);
                db.SaveChanges();
                ModelState.Clear();
                return View();
            }
            return View();
        }



        public IActionResult StoreUpdate(int id)
        {
            var store = db.Stores.Find(id);
            
            return View(store);
        }

        [HttpPost]
        public IActionResult StoreUpdate(Store store)
        {
            if (ModelState.IsValid)
            {
                db.Stores.Update(store);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult StoreDetail(int id)
        {
            var vm = new SiteInfoViewModel();
            vm.Store = db.Stores.Find(id);
            vm.SiteInfo = db.SiteInfos.Include(p=>p.Store).Where(p=>p.StoreId == id).ToList();
            return View(vm);
        }

        public IActionResult StoreDelete(int id)
        {
            var store = db.Stores.Find(id);
            db.Stores.Remove(store);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        #endregion

        #region Store

        public IActionResult SiteInfoAdd()
        {
            
            ViewBag.store = new SelectList(db.Stores.ToList(), "Id", "Name");
            
            return View();
        }

        [HttpPost]
        [Obsolete]
        public IActionResult SiteInfoAdd(IFormFile? fileLogo, IFormFile? fileIcon, SiteInfo siteInfo, List<SocialLink> SocialLink)
        {
            ViewBag.store = new SelectList(db.Stores.ToList(), "Id", "Name");
            if (fileLogo == null)
            {
                return View(siteInfo);
            }
            if (fileIcon == null)
            {
                return View(siteInfo);
            }
            if (fileIcon != null)
            {
              
                siteInfo.FavIcon = Image.Add(fileIcon, env);
            }
            if (fileLogo != null)
            {
               
                siteInfo.Logo = Image.Add(fileLogo, env);
            }
            db.SiteInfos.Add(siteInfo);
            db.SaveChanges();
            foreach (var item in SocialLink)
            {
                db.SocialLinks.Add(item);
                db.SaveChanges();
                var siteSocial = new SiteSocial();
                siteSocial.SocialId = item.Id;
                siteSocial.SiteInfoId = siteInfo.Id;
                siteSocial.Id = item.Id;
                db.SiteSocial.Add(siteSocial);
                db.SaveChanges();
                ModelState.Clear();
            }


                ModelState.Clear(); 
                return View();
        }



        public IActionResult SiteInfoUpdate(int id)
        {
            var fm = new SiteInfoFormModel();
            
            ViewBag.store = new SelectList(db.Stores.ToList(), "Id", "Name");
            fm.SiteInfo = db.SiteInfos.Find(id);
            fm.SiteSocials = db.SiteSocial.Include(p=>p.Social).Where(p => p.SiteInfoId == id).ToList();
            return View(fm);
        }

        [HttpPost]
        [Obsolete]
        public IActionResult SiteInfoUpdate(IFormFile? fileLogo, IFormFile? fileIcon, SiteInfoFormModel fm, SiteInfo siteInfo, List<SocialLink> SocialLink)
        {
            siteInfo = fm.SiteInfo;
            ViewBag.store = new SelectList(db.Stores.ToList(), "Id", "Name");
            if (fileLogo == null && siteInfo.Logo == null)
            {
                return View(siteInfo);
            }
            if (fileIcon == null && siteInfo.FavIcon == null)
            {
                return View(siteInfo);
            }
            if (fileIcon != null)
            {
                Image.Delete(siteInfo.FavIcon, env);
                siteInfo.FavIcon = Image.Add(fileIcon, env);
            }
            if (fileLogo != null)
            {
                Image.Delete(siteInfo.Logo, env);
                siteInfo.Logo = Image.Add(fileLogo, env);
            }



                db.SiteInfos.Update(siteInfo);

            foreach (var item in SocialLink)
            {
                db.SocialLinks.Add(item);
                db.SaveChanges();
                var siteSocial = new SiteSocial();
                siteSocial.SocialId = item.Id;
                siteSocial.SiteInfoId = siteInfo.Id;
                siteSocial.Id = item.Id;
                db.SiteSocial.Add(siteSocial);
                db.SaveChanges();
                ModelState.Clear();
            }
                return RedirectToAction("index");
        }

        [Obsolete]
        public IActionResult SiteInfoDelete(int id)
        {
            var site = db.SiteInfos.Find(id);
            
            Image.Delete(site.FavIcon, env);
            Image.Delete(site.Logo, env);
            db.SiteInfos.Remove(site);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        #endregion

        #region Social
        public IActionResult SocialUpdate(int id, int infoid)
        {
          
            var social = db.SocialLinks.Find(id);
            ViewBag.info = infoid;
            return View(social);
        }

        [HttpPost]
        public IActionResult SocialUpdate(SocialLink social, int infoid)
        {
            db.SocialLinks.Update(social);

            db.SaveChanges();
          
            return RedirectToAction("SiteInfoUpdate", new{id=infoid});
        }

        public IActionResult SocialDelete(int id, int infoid)
        {

            var social = db.SocialLinks.Find(id);
            db.SocialLinks.Remove(social);
            db.SaveChanges();
            return RedirectToAction("SiteInfoUpdate", new { id = infoid });
        }
        #endregion
    }
}
