using FinalProjectWebUI.Models.Entity;

namespace FinalProjectWebUI.Models.FormModel
{
    public class SiteInfoFormModel
    {
        public SiteInfo SiteInfo { get; set; }
        public IEnumerable<SiteSocial> SiteSocials { get; set; }
    }
}
