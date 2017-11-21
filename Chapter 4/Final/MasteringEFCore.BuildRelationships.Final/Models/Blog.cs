using System.Collections.Generic;

namespace MasteringEFCore.BuildRelationships.Final.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public ICollection<Post> Posts { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
    }
}