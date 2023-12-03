using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BankController(ApplicationDbContext context)
        {
            _context = context;
        }
        long Recievecardnumberbanck1 = 1212444455556666;
        long Recievecardnumberbanck2 = 1313444455557777;
        long Recievecardnumberbanck3 = 1414444455556666;
        public int GetBankNumber(long bankNumber)
        {
            string numberString = bankNumber.ToString(); // Convert the number to a string
            string firstFourDigits = numberString.Substring(0, 4);
            if (firstFourDigits == "1212")
            {
                return 1;
            }
            else if (firstFourDigits == "1313")
            {
                return 2;
            }
            else if (firstFourDigits == "1414")
            {
                return 3;
            }
            else
            {
                return -1;
            }
        }

        [HttpPost]
        [Route("CreateAccount")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> CreateAccount([FromBody] Account Account)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            string numberString = Account.BankCardNumber.ToString();

            string firstfourdigits = numberString.Substring(0, 4);

            if (firstfourdigits == "1212" && _context.FirstBank.FirstOrDefault(b=>b.BankCardNumber==Account.BankCardNumber)==null)
            {
                FirstBank firstbank = new FirstBank
                {
                    BankCardNumber = Account.BankCardNumber,
                    Balance = Account.Balance,
                    Name = Account.Name,
                    Password = Account.Password

                };
                _context.FirstBank.Add(firstbank);
                await _context.SaveChangesAsync();
                Massage response = new Massage
                {
                    massage = "Create account in Firstbank Susseccfull"

                };
                return Ok(response);
            }
            else if (firstfourdigits == "1313" && _context.SecondBank.FirstOrDefault(b => b.BankCardNumber == Account.BankCardNumber) == null)
            {
                SecondBank SecondBank = new SecondBank
                {
                    BankCardNumber = Account.BankCardNumber,
                    Balance = Account.Balance,
                    Name = Account.Name,
                    Password = Account.Password

                };
                _context.SecondBank.Add(SecondBank);
                await _context.SaveChangesAsync();
                Massage response = new Massage
                {
                    massage = "Create account in Secondbank Susseccfull"

                };
                return Ok(response);
            }
            else if (firstfourdigits == "1414" && _context.ThirdBank.FirstOrDefault(b => b.BankCardNumber == Account.BankCardNumber) == null)
            {
                ThirdBank ThirdBank = new ThirdBank
                {
                    BankCardNumber = Account.BankCardNumber,
                    Balance = Account.Balance,
                    Name = Account.Name,
                    Password = Account.Password

                };
                _context.ThirdBank.Add(ThirdBank);
                await _context.SaveChangesAsync();
                Massage response = new Massage
                {
                    massage = "Create account in Thirdbank Susseccfull"

                };
                return Ok(response);
            }
            Massage error = new Massage
            {
                massage = "The number of card should have to only one user !"

            };
            return BadRequest(error);
        }
        [HttpPost]
        [Route("VieweAccount")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> VieweAccount(long BankCardNumber)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            string numberString = BankCardNumber.ToString();

            string firstfourdigits = numberString.Substring(0, 4);

            if (firstfourdigits == "1212" && _context.FirstBank.FirstOrDefault(b => b.BankCardNumber == BankCardNumber) != null)
            {
                viewaccount firstbank = new viewaccount
                {
                   
                    Balance = _context.FirstBank.FirstOrDefault(a=>a.BankCardNumber==BankCardNumber).Balance,
                    Name = _context.FirstBank.FirstOrDefault(a => a.BankCardNumber == BankCardNumber).Name,

                };
                
                return Ok(firstbank);
            }
            else if (firstfourdigits == "1313" && _context.SecondBank.FirstOrDefault(b => b.BankCardNumber ==BankCardNumber) != null)
            {
                viewaccount SecondBank = new viewaccount
                {

                    Balance = _context.SecondBank.FirstOrDefault(a => a.BankCardNumber == BankCardNumber).Balance,
                    Name = _context.SecondBank.FirstOrDefault(a => a.BankCardNumber == BankCardNumber).Name,

                };
                
                return Ok(SecondBank);
            }
            else if (firstfourdigits == "1414" && _context.ThirdBank.FirstOrDefault(b => b.BankCardNumber ==BankCardNumber) != null)
            {
                viewaccount ThirdBank = new viewaccount
                {

                    Balance = _context.ThirdBank.FirstOrDefault(a => a.BankCardNumber == BankCardNumber).Balance,
                    Name = _context.ThirdBank.FirstOrDefault(a => a.BankCardNumber == BankCardNumber).Name,

                };
                return Ok(ThirdBank);
            }
            Massage error = new Massage
            {
                massage = "user does not have account !"

            };
            return BadRequest(error);
        }


        [HttpPost]
        [Route("withdraw")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> withdraw([FromBody] withdrawanddeposit BankModelView)
        {


            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var shoppingcart1 = _context.Shopping_Carts.FirstOrDefault(cart => cart.UserId == userId);

            decimal Amount = _context.Cart_Items
               .Where(p => p.Shopping_Carts == shoppingcart1)
               .Select(p => p.Product.Price * p.Quantity)
               .Sum();
            var SenderBank = GetBankNumber(BankModelView.SenderBankNumber);
            if (SenderBank == -1)
            {
                Massage response = new Massage
                {
                    massage = "Incorrect Bank Number"
                };
                return BadRequest(response);
            }
            if (SenderBank == 1)
            {
                var SenderAccount = _context.FirstBank.Where(x => x.BankCardNumber == BankModelView.SenderBankNumber).FirstOrDefault();

                if (SenderAccount.Balance >= Amount)
                {
                    SenderAccount.Balance -= Amount;
                    _context.SaveChanges();

                    Massage response = new Massage
                    {
                        massage = "The Process Succefully Done!" + Amount + " " + SenderAccount.Balance
                    };
                    return Ok(response);
                }
                else
                {
                    Massage response = new Massage
                    {
                        massage = "Not enough budget"
                    };
                    return BadRequest(response);
                }
            }
            if (SenderBank == 2)
            {
                var SenderAccount = _context.SecondBank.Where(x => x.BankCardNumber == BankModelView.SenderBankNumber).FirstOrDefault();
                if (SenderAccount.Balance >= Amount)
                {
                    SenderAccount.Balance -= Amount;
                    _context.SaveChanges();

                    Massage response = new Massage
                    {
                        massage = "The Process Succefully Done1" + Amount + " " + SenderAccount.Balance
                    };
                    return Ok(response);
                }
                else
                {
                    Massage response = new Massage
                    {
                        massage = "Not enough budget"
                    };
                    return BadRequest(response);
                }

            }
            if (SenderBank == 3)
            {
                var SenderAccount = _context.ThirdBank.Where(x => x.BankCardNumber == BankModelView.SenderBankNumber).FirstOrDefault();

                if (SenderAccount.Balance >= Amount)
                {
                    SenderAccount.Balance -= Amount;
                    _context.SaveChanges();
                    Massage response = new Massage
                    {
                        massage = "The Process Succefully Done2" + Amount + " " + SenderAccount.Balance
                    };
                    return Ok(response);
                }
                else
                {
                    Massage response = new Massage
                    {
                        massage = "Not enough budget"
                    };
                    return BadRequest(response);
                }
            }

            return Ok();

        }
        [HttpPost]
        [Route("deposit")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> deposit([FromBody] withdrawanddeposit BankModelView)
        {


            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var shoppingcart1 = _context.Shopping_Carts.FirstOrDefault(cart => cart.UserId == userId);

            decimal Amount = _context.Cart_Items
               .Where(p => p.Shopping_Carts == shoppingcart1)
               .Select(p => p.Product.Price * p.Quantity)
               .Sum();
            var SenderBank = GetBankNumber(BankModelView.SenderBankNumber);
            if (SenderBank == -1)
            {
                Massage response = new Massage
                {
                    massage = "Incorrect Bank Number"
                };
                return BadRequest(response);
            }
            if (SenderBank == 1)
            {
              
                    var ReciverAccount = _context.FirstBank.Where(x => x.BankCardNumber == Recievecardnumberbanck1).FirstOrDefault();
                    ReciverAccount.Balance += Amount; 
                    _context.SaveChanges();

                    Massage response = new Massage
                    {
                        massage = "The Process Succefully Done!" + Amount + " "+ ReciverAccount.Balance
                    };
                    return Ok(response);
            
            }
            if (SenderBank == 2)
            {
              
                    var ReciverAccount = _context.SecondBank.Where(x => x.BankCardNumber == Recievecardnumberbanck2).FirstOrDefault();
                    ReciverAccount.Balance += Amount; 
                    _context.SaveChanges();

                    Massage response = new Massage
                    {
                        massage = "The Process Succefully Done1" + Amount + " " + ReciverAccount.Balance
                    };
                    return Ok(response);
                
           
            }
            if (SenderBank == 3)
            {

                
                    var ReciverAccount = _context.ThirdBank.Where(x => x.BankCardNumber == Recievecardnumberbanck3).FirstOrDefault();
                    ReciverAccount.Balance += Amount;
                    _context.SaveChanges();
                    Massage response = new Massage
                    {
                        massage = "The Process Succefully Done2" + Amount + " "+ ReciverAccount.Balance
                    };
                    return Ok(response);
                
            }

            return Ok();

        }

    }
}
