﻿using System.ComponentModel.DataAnnotations;

namespace backend.Models.Dto
{
    public class LoginDto
    {
        [Required] 
        public string Email { get; set; }

        [Required] 
        public string Password { get; set; } 
    }
}
