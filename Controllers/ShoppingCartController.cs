using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApplication5.Data;
using WebApplication5.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;



        public ShoppingCartController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("addtocart")]
        public async Task<IActionResult> AddToCart([FromBody] CartitemsModelView CartitemsModelView)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var product = _context.Product.FirstOrDefault(p => p.ProductId == CartitemsModelView.ProductId);

            // Check if the product exists.
            if (product == null)
            {
                Massage response1 = new Massage
                {
                    massage = "the Product does not exist !"

                };
                return BadRequest(response1);


            }
            var shoppingcart = _context.Shopping_Carts.FirstOrDefault(sh => sh.Shopping_CartsId == CartitemsModelView.Shopping_CartsId && sh.UserId == userId);
            if (shoppingcart == null)
            {
                Massage response1 = new Massage
                {
                    massage = "the cart does not exist !"

                };
                return BadRequest(response1);
            }
            else
            {
                Cart_Items cartitems = new Cart_Items
                {
                    Quantity = CartitemsModelView.Quantity,
                    Shopping_CartsId = CartitemsModelView.Shopping_CartsId,
                    ProductId = CartitemsModelView.ProductId
                };

                _context.Cart_Items.Add(cartitems);
                await _context.SaveChangesAsync();
                Massage response = new Massage
                {
                    massage = "Susseccfull"

                };
                return Ok(response);
            }
            return Ok();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Viewcart")]
        public async Task<ActionResult> ViewCart()
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

            var shoppingcart = _context.Shopping_Carts.FirstOrDefault(sh => sh.UserId == userId);
            if (shoppingcart == null)
            {
                Massage response1 = new Massage
                {
                    massage = "the cart does not exist !"

                };
                return BadRequest(response1);
            }

            var cartitems = _context.Cart_Items.Where(sh => sh.Shopping_CartsId == shoppingcart.Shopping_CartsId);
            var viewcartmodelviews = cartitems.Select(cartitem =>

            new ViewCartModelview
            {
                Cart_ItemsId = cartitem.Cart_ItemsId,
                Quantity = cartitem.Quantity,
                Name = _context.Product.FirstOrDefault(u => u.ProductId == cartitem.ProductId).Name,
                Price = _context.Product.FirstOrDefault(u => u.ProductId == cartitem.ProductId).Price,
                Image = _context.Product.FirstOrDefault(u => u.ProductId == cartitem.ProductId).Image,

            });
            var shoppingcart1 = _context.Shopping_Carts.FirstOrDefault(cart => cart.UserId == userId);

            var totalCost = _context.Cart_Items
               .Where(p => p.Shopping_Carts == shoppingcart1)
               .Select(p => p.Product.Price * p.Quantity)
               .Sum();
            var cart = new cartChildViewModel
            {
                cart = viewcartmodelviews.ToList(),
                TotalPrice = totalCost
            };
            return Ok(cart);
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("removefromcart")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            
            var cartItem = await _context.Cart_Items.FirstOrDefaultAsync(c => c.Cart_ItemsId == cartItemId);

            _context.Cart_Items.Remove(cartItem);

           
            await _context.SaveChangesAsync();

         
            Massage response = new Massage
            {
                massage = "Product removed from cart successfully"
            };
            return Ok(response);
        }




    }
}
