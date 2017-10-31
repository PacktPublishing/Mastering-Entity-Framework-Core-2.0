using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Concurrencies.Starter.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Subtitle is required")]
        public string Subtitle { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Url is required")]
        [Url(ErrorMessage = "Provide a valid url")]
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        [JsonIgnore]
        public ICollection<Post> Posts { get; set; }
    }
}
