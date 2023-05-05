using FinalProjectWebUI.Models.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebUI.Components
{
    public class CollectionViewComponent : ViewComponent
    {
        readonly FinalDbContext db;
        public CollectionViewComponent(FinalDbContext db)
        {
            this.db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync(string? order, bool isfirst)
        {

            var collections = db.Collections.Include(p => p.Collections).Where(p=> p.Collections.Count() > 0).ToList();

            if(isfirst)
            {
                ViewBag.First = "first";
            }
            return View(collections);

        }
    }
}
