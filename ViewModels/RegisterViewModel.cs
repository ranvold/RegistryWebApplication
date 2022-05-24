﻿using System.ComponentModel.DataAnnotations;

namespace RegistryWebApplication.ViewModels

{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Year of birth")]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password don't match")]
        [Display(Name = "Password confirm")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
