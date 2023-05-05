using System.ComponentModel.DataAnnotations;

namespace FinalProjectWebUI.Models.ViewModel
{
    public class RoleViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Id { get; set; }

    }
}
