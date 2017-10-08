using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MasteringEFCore.QueryObjectPattern.Starter.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Display Name is required")]
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [Remote(action: "IsUsernameAvailable", controller:"Users")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; }
        [EmailAddress(ErrorMessage = "Provide a valid email address")]
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public int? AddressId { get; set; }
        public Address Address { get; set; }
        [Required(ErrorMessage = "Person is required")]
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
