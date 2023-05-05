using FinalProjectWebUI.Models.Entity;

namespace FinalProjectWebUI.Models.ViewModel
{
    public class ColorSizeViewModel
    {
        public IEnumerable<Color> Colors { get; set; }
        public IEnumerable<Size> Sizes { get; set; }
        public IEnumerable<Genders> Genders { get; set; }
    }
}
