using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.RawSql.Final.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }

        public ICollection<Category> Subcategories { get; set; }
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
