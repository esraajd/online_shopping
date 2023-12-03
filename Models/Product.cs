using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApplication5.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
      
        [Required]
        public decimal Price { get; set; }
        public string Image { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }


    }
    public class ProductViewModel
    {
        public int ProductId { get; set; }
    
        public string Name { get; set; }
        public string Description { get; set; }

   
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string CategoryName { get; set; }

    }

}
