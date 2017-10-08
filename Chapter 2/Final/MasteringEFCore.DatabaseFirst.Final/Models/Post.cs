using System;
using System.Collections.Generic;

namespace MasteringEFCore.DatabaseFirst.Final.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDateTime { get; set; }
        public string Title { get; set; }

        public virtual Blog Blog { get; set; }
    }
}
