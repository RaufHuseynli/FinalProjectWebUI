using FinalProjectWebUI.Models.Entity;

namespace FinalProjectWebUI.Models.ViewModel
{
    public class FooterViewModel
    {
        public SiteInfo SiteInfo { get; set; }
        public IEnumerable<SiteSocial> SiteSocials { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Store> Stores { get; set; }
    }
}
