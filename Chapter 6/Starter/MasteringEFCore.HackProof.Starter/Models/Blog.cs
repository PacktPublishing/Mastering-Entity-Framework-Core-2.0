using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MasteringEFCore.HackProof.Starter.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Subtitle is required")]
        public string Subtitle { get; set; }
        [Required(ErrorMessage = "Url is required")]
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public ICollection<Post> Posts { get; set; }
        [Required(ErrorMessage = "Author is required, kindly pick one!")]
        public int AuthorId { get; set; }
        public User Author { get; set; }
    }
}