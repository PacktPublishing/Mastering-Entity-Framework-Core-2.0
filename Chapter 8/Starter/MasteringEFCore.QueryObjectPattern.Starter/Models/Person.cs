using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.QueryObjectPattern.Starter.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabets were allowed")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabets were allowed")]
        public string LastName { get; set; }
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabets were allowed")]
        public string NickName { get; set; }
        [Url(ErrorMessage = "Provide a valid url")]
        public string Url { get; set; }
        public string Biography { get; set; }
        [Url(ErrorMessage = "Provide a valid url")]
        public string ImageUrl { get; set; }
        [Phone(ErrorMessage = "Provide a valid phone number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
