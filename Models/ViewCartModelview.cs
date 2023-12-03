using System.Collections.Generic;

namespace WebApplication5.Models
{
    public class ViewCartModelview
    {
        public int Cart_ItemsId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
    public class cartChildViewModel
    {
        public List<ViewCartModelview> cart { get; set; }

        public decimal TotalPrice { get; set; }

    }
}