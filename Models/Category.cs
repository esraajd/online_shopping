using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Category
    {

        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> Children { get; set; }
    }
    public class CategoryModelView
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }
        public int? ParentId { get; set; }

        public List<CategoryModelView> Children { get; set; }

    }
    public class CategoryParentModelView
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }
        public int? ParentId { get; set; }


    }


}   