using MasteringEFCore.RawSql.Starter.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MasteringEFCore.RawSql.Starter.Models
{
    public class Post : IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
        public string Summary { get; set; }
        [FutureOnly]
        public DateTime PublishedDateTime { get; set; }
        public string Url { get; set; }
        public long VisitorCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        [Required(ErrorMessage = "Blog is required")]
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        [Required(ErrorMessage = "Author is required")]
        public int AuthorId { get; set; }
        public User Author { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<TagPost> TagPosts { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext
         validationContext)
        {
            var post = (Post)validationContext.ObjectInstance;
            if (post.PublishedDateTime.CompareTo(DateTime.Now) < 0)
                yield return
                  new ValidationResult("Publishing Date cannot be in past," +
                  "kindly provide a future date", new []{ "PublishedDateTime"
             });
      }
}
}