using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Performance.Starter.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        //[ConcurrencyCheck]
        public DateTime ModifiedAt { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        [JsonIgnore]
        public ICollection<TagPost> TagPosts { get; set; }
        //[Timestamp]
        //public byte[] Timestamp { get; set; }
    }
}
