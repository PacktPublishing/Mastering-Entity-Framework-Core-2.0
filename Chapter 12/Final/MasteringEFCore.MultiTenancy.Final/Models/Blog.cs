using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Final.Models
{
    public class Blog : EntityBase
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Subtitle is required")]
        public string Subtitle { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Url is required")]
        [Url(ErrorMessage = "Provide a valid url")]
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
        //[ConcurrencyCheck]
        public DateTime ModifiedAt { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int? CategoryId { get; set; }

        [JsonIgnore]
        public ICollection<Post> Posts { get; set; }
        //[Timestamp]
        //public byte[] Timestamp { get; set; }
    }
}
