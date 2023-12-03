using Microsoft.AspNetCore.Http;

namespace WebApplication5.Models
{
    public class Productimg
    {

        public string Name { get; set; }
        public string Description { get; set; }


        public decimal Price { get; set; }
        public IFormFile img { get; set; }
        public int CategoryId { get; set; }
    }
}
