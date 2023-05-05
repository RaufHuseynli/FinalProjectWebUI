using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectWebUI.Models.Entity
{
    public class SiteSocial
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int SiteInfoId { get; set; }
        public SiteInfo SiteInfo { get; set; }

        public int SocialId { get; set; }
        public SocialLink Social { get; set; }
    }
}
