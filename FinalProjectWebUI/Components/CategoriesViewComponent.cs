using FinalProjectWebUI.Models.DataContext;
using FinalProjectWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebUI.Components
{
    public class CategoriesViewComponent : ViewComponent
    {
        readonly FinalDbContext db;
        public CategoriesViewComponent(FinalDbContext db)
        {
            this.db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync(string? order)
        {
            var categories = db.Categories.Include(p=>p.Products).Where(p=>p.Products.Count > 0).ToList();
            return View(categories);
        }

    }
}
