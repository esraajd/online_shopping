using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class SecondBank
    {

        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public long BankCardNumber { get; set; }
        public decimal Balance { get; set; }
        public int Password { get; set; }
    }
}
