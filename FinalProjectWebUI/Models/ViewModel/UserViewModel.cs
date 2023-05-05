﻿using System.ComponentModel.DataAnnotations;

namespace FinalProjectWebUI.Models.ViewModel
{
    public class UserViewModel
    {
        [Required]
        public string UserName { get; set; }
     
        [Required]
        public string Email { get; set; }
        [Required]
        public String Password { get; set; }



 
    }
}
