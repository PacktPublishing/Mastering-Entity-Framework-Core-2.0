using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MasteringEFCore.HackProof.Starter.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Display Name is required")]
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; }
        [Required(ErrorMessage = "Email is required")]
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
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}