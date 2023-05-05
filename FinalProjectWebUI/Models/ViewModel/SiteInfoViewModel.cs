using FinalProjectWebUI.Models.Entity;

namespace FinalProjectWebUI.Models.ViewModel
{
    public class SiteInfoViewModel
    {
        public Store Store { get; set; }
        public IEnumerable<Store> Stores { get; set; }
        public IEnumerable<SiteInfo> SiteInfo { get; set; }
        public IEnumerable<SiteSocial> SiteSocials { get; set; }
    }
}
