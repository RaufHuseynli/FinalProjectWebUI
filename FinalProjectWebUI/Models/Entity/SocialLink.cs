using System.ComponentModel.DataAnnotations;

namespace FinalProjectWebUI.Models.Entity
{
    public class SocialLink:BaseEntity
    {
        [Required]
        public string Icon { get; set; }

        [Required(ErrorMessage = "The name must be written")]
        public string Name { get; set; }
        public string Link { get; set; }
        public ICollection<SiteSocial> SiteSocial { get; set; }
    }
}
