using System.Collections.Generic;

namespace MasteringEFCore.BuildRelationships.Final.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Blogs { get; set; }
        public Address Address { get; set; }
    }
}