using System;
using System.Collections.Generic;

namespace MasteringEFCore.Relationship.Final.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public DateTime PublishedDateTime { get; set; }
        public string Url { get; set; }
        public string BlogUrl { get; set; }
        public long VisitorCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int BlogId { get; set; }

        //public int BlogSomeId { get; set; }
        //[ForeignKey("BlogSomeId")]
        //public int SomeBlogId { get; set; }
        //public Blog SomeBlog { get; set; }
        public Blog Blog { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<TagPost> TagPosts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}