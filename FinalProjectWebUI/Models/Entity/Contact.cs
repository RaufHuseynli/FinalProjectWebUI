using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectWebUI.Models.Entity
{
    public class Contact :BaseEntity
    {


        [Required]
        public string FullName { get; set; }


        [Required]
        public string Subject { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }


        [Required]
        public bool Isanswerd { get; set; }

    }
}
