using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using MimeKit;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender emailSender;
        public OrdersController(ApplicationDbContext context, UserManager<User> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            this.emailSender = emailSender;
        }

        //[HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[Route("AddOrder")]
        //public async Task<IActionResult> AddOrder([FromBody] CheckoutModel CheckoutModel)
        //{
        //    string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;


        //    // Check if the product exists.


        //    Orders orders = new Orders
        //    {
        //        CustomerId = userId,
        //        Order_Date = DateTime.Now,
        //        Order_Status = OrdersStatus.Pending,
        //        Shipping_adress = CheckoutModel.Shipping_adress,
        //        Billing_address = CheckoutModel.Billing_address,
        //        Paymnet_Method = CheckoutModel.Paymnet_Method
        //    };

        //    _context.Orders.Add(orders);
        //    await _context.SaveChangesAsync();
        //    Massage response = new Massage
        //    {
        //        massage = "Susseccfull"

        //    };
        //    return Ok(response);

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Confirmorder")]
        public async Task<IActionResult> Confirmorder([FromBody] CheckoutModel CheckoutModel)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("User is not authenticated");
            }


            var shoppingcart = _context.Shopping_Carts.FirstOrDefault(cart => cart.UserId == userId);

            decimal totalCost = _context.Cart_Items
                .Where(p => p.Shopping_Carts == shoppingcart)
                .Select(p => p.Product.Price * p.Quantity)
                .Sum();
            // Create a new order in the database.
            var order = new Orders
            {
                CustomerId = userId,
                Order_Date = DateTime.Now.AddDays(14),
                Order_Status = OrdersStatus.Pending,
                Shipping_adress = CheckoutModel.Shipping_adress,
                TotalCost= totalCost,
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            var CheckoutModelView = new CheckoutModelView
            {
                Order_Date = "Expected Date :"+" "+order.Order_Date.ToString(),
                Order_Status = order.Order_Status,
                TotalCost = order.TotalCost,
                Shipping_adress = order.Shipping_adress


            };
           
            
            return Ok(CheckoutModelView);
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Vieworder")]
        public async Task<ActionResult> Vieworder()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                Massage response = new Massage
                {
                    massage = "Invalid User Id"
                };
                return BadRequest(response);
            }

            var order = _context.Orders.FirstOrDefault(o => o.CustomerId == userId);
            if (order == null)
            {
                Massage response1 = new Massage
                {
                    massage = "No orders yet !"

                };
                return BadRequest(response1);
            }
    

            var vieworder =new CheckoutModelView
            {
                Order_Date = "Expected Date :" + " " + order.Order_Date.ToString(),
                Order_Status = order.Order_Status,
                TotalCost =order.TotalCost,
                Shipping_adress = order.Shipping_adress
            };

           
            return Ok(vieworder);
        }





        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Cancelled")]
        public async Task<IActionResult> Cancelled([FromBody] OrderStatus OrderStatus)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;


            var Order = await _context.Orders.FirstOrDefaultAsync(c => c.OrdersId == OrderStatus.OrderId);



            if (Order.Order_Status == OrdersStatus.Shipped || Order.Order_Status == OrdersStatus.Processing)
            {
                Order.Order_Status = OrdersStatus.Cancelled;
                _context.Update(Order);

                await _context.SaveChangesAsync();
                Massage response = new Massage
                {
                    massage = "Cancelled"
                };
                return Ok(response);
            }
            return BadRequest();


        }
    }
}
    

