using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.RawSql.Final.ViewModels
{
    public class RegistrationViewModel
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "Username is required")]
        [MinLength(6, ErrorMessage = "Username needs minimum 6 characters")]
        [MaxLength(30, ErrorMessage = "Username cannot exceed 30 characters")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword is required")]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Provide a valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Provide a valid email address")]
        [Compare("Email", ErrorMessage = "Email does not match")]
        public string ConfirmEmail { get; set; }
    }
}
