using FinalProjectWebUI.Models.Entity;
using FinalProjectWebUI.Models.FormModel;

namespace FinalProjectWebUI.Models.ViewModel
{
    public class FilterViewModel
    {
        public IEnumerable<Color> Colors { get; set; }
        public IEnumerable<Size> Sizes { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Genders> Genders { get; set; }
        public FilterFormModel FilterFormModel { get; set; }
    }
}
