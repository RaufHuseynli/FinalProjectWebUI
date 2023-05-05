using FinalProjectWebUI.Models.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebUI.Components
{
    public class FooterViewComponent : ViewComponent
    {
        readonly FinalDbContext db;
        public FooterViewComponent(FinalDbContext db)
        {
            this.db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync(int categorystore)
        {
            if (categorystore == 0)
            {
                categorystore = 1;
            }
            var store = db.SiteInfos.FirstOrDefault(p=> p.StoreId == categorystore);
            var social = db.SiteSocial.Include(p=>p.Social).Where(p => p.SiteInfoId == store.Id).ToList();
            return View(social);

        }
    }
}
