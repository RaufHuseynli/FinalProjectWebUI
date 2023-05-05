using FinalProjectWebUI.Models.Entity;

namespace FinalProjectWebUI.Models.ViewModel
{
    public class CategoryCollectionViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Collection> Collections { get; set; }
    }
}
