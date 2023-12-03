using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Orders
    {
        [Key]
        public int OrdersId { get; set; }
        public string CustomerId { get; set; }
        public User Customer { get; set; }
        public DateTime Order_Date { get; set; }

        public string Order_Status { get; set; }
        public string Shipping_adress { get; set; }
        public string Billing_address { get; set; }
        public string Paymnet_Method { get; set; }
        public decimal TotalCost { get; set; }
    }
    public class CheckoutModel
    {
        
        
        public string CustomerId { get; set; }
        public DateTime Order_Date { get; set; }

        public string Order_Status { get; set; }
        public string Shipping_adress { get; set; }
        public long CardNumber { get; set; }
        public decimal TotalCost { get; set; }   


    }
    public class CheckoutModelView
    {


        public string Order_Date { get; set; }

        public string Order_Status { get; set; }
        public string Shipping_adress { get; set; }
        public decimal TotalCost { get; set; }


    }

}
