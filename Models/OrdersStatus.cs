namespace WebApplication5.Models
{
    public class OrdersStatus
    {
        public static string Cancelled = "Cancelled";

        public static string Processing = "Processing";

        public static string Pending = "Pending";
        public static string Shipped = "Shipped";

        public static string Delivered = "Delivered";

    }
    public class OrderStatus
    {
        public int OrderId { get; set; }

        public string Status { get; set; }

    }
}
