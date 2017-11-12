using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MasteringEFCore.Concurrencies.Final.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace MasteringEFCore.Concurrencies.Final.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
        public string Summary { get; set; }
        [FutureOnly]
        public DateTime PublishedDateTime { get; set; }

        //[Url(ErrorMessage = "Provide a valid url")]
        //[ConcurrencyCheck]
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

        [JsonIgnore]
        public ICollection<TagPost> TagPosts { get; set; }
        [JsonIgnore]
        public ICollection<Comment> Comments { get; set; }

        [NotMapped]
        public ICollection<Tag> Tags { get; set; }
        [NotMapped]
        public ICollection<int> TagIds { get; set; }
        [NotMapped]
        public string TagNames { get; set; }
        [Display(Name = "Image Name")]
        public Guid FileId { get; set; }
        [NotMapped]
        public string FileName { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
