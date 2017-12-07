using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MasteringEFCore.HackProof.Final.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public ICollection<TagPost> TagPosts { get; set; }
    }
}