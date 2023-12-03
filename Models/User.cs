using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public string Address { get; set; }
        public Shopping_Carts Shopping_Carts{ get; internal set; }


    }
}
