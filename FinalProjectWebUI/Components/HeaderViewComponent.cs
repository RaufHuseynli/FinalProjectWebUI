using FinalProjectWebUI.Models.DataContext;
using FinalProjectWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebUI.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        readonly FinalDbContext db;
        public HeaderViewComponent(FinalDbContext db)
        {
            this.db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync(string? order, int categorystore)
        {
            var vm = new FooterViewModel();
            if(categorystore== 0)
            {
                categorystore= 1;
            }
            vm.SiteInfo = db.SiteInfos.FirstOrDefault(p=> p.StoreId == categorystore);
            vm.SiteSocials = db.SiteSocial.Where(p => p.SiteInfoId == vm.SiteInfo.Id);
            vm.Categories = db.Categories.Include(p=> p.Products).Where(p=> p.Products.Count > 0).ToList();
            vm.Stores = db.Stores.Include(p=> p.SiteInfos).Where(p=> p.SiteInfos.Count() > 0).ToList();
            ViewBag.Order = order;


            return View(vm);

        }
    }
}
