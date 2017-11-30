using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MasteringEFCore.Validations.Final.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }

        public ICollection<Post> Posts { get; set; }
        [Required(ErrorMessage = "Author is required, kindly pick one!")]
        public int AuthorId { get; set; }
        public User Author { get; set; }
    }
}