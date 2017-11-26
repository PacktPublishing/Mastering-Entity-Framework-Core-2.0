using System.Collections.Generic;

namespace MasteringEFCore.Validations.Starter.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TagPost> TagPosts { get; set; }
    }
}