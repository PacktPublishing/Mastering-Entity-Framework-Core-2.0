using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasteringEFCore.Relationship.Final.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public ICollection<Post> Posts { get; set; }
        //[InverseProperty("SomeBlog")]
        //public ICollection<Post> SomePosts { get; set; }
    }
}