using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Cart_Items
    {
        [Key]
        public int Cart_ItemsId { get; set; }
        public int Quantity { get; set; }
        public int Shopping_CartsId { get; set; }
        public Shopping_Carts Shopping_Carts { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }    


    }
    public class CartitemsModelView
    {
        
       
        public int Quantity { get; set; }
        public int Shopping_CartsId { get; set; }
        public int ProductId { get; set; }


    }
}
