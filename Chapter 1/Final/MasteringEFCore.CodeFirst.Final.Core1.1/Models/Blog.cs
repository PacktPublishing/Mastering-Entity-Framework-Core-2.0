using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.CodeFirst.Final.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
