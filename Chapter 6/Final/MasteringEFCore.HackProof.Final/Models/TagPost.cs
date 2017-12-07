using System;
using System.ComponentModel.DataAnnotations;

namespace MasteringEFCore.HackProof.Final.Models
{
    public class TagPost
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        [Required(ErrorMessage = "Tag is required")]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        [Required(ErrorMessage = "Post is required")]
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}