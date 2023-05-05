using FinalProjectWebUI.Models.DataContext;
using FinalProjectWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebUI.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        readonly FinalDbContext db;
        public CategoryViewComponent(FinalDbContext db)
        {
            this.db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync(string? order)
        {
            var vm = new FilterViewModel();
             vm.Categories = db.Categories.ToList();
             vm.Colors = db.Color.ToList();
            vm.Sizes = db.Sizes.ToList();
            vm.Genders = db.Genders.ToList();
            return View(vm);

        }


    }
}