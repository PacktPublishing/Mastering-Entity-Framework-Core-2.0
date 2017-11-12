using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Concurrencies.Final.Models
{
    public class TagPost
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tag is required")]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        [Required(ErrorMessage = "Post is required")]
        public int PostId { get; set; }
        public Post Post { get; set; }
        public DateTime CreatedAt { get; set; }
        //[ConcurrencyCheck]
        public DateTime ModifiedAt { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        //[Timestamp]
        //public byte[] Timestamp { get; set; }
    }
}
    