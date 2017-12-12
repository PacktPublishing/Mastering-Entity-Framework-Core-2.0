using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MasteringEFCore.Validations.Final.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        public string NickName { get; set; }
        [Url(ErrorMessage = "Provide a valid url")]
        public string Url { get; set; }
        public string Biography { get; set; }
        [Url(ErrorMessage = "Provide a valid image url")]
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}