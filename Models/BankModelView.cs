using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class withdrawanddeposit
    {
        public long SenderBankNumber { get; set; }
    }
   
    public class Account
    {
        [Required]

        public string Name { get; set; }
        [Required]

        public long BankCardNumber { get; set; }
        [Required]

        public decimal Balance { get; set; }
        [Required]

        public int Password { get; set; }
    }
    public class viewaccount
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}
