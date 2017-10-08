using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
