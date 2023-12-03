using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Shopping_Carts
    {
        [Key]
        public int Shopping_CartsId { get; set; }
        public DateTime Created_at { get; set; }

        //relation with user
        public string UserId { get; set; }
        public User Customer { get; set; }
        

    }
  
}
