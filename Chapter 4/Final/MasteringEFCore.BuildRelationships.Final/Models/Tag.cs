using System.Collections.Generic;

namespace MasteringEFCore.BuildRelationships.Final.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TagPost> TagPosts { get; set; }
    }
}