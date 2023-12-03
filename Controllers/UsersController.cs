using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.Linq;
using System.Threading.Tasks;
using System;
using WebApplication5.Data;
using WebApplication5.Models;
using Newtonsoft.Json.Linq;
using WebApplication5.Services;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<User> userManager;
        private readonly IConfiguration _configration;
        private IWebHostEnvironment Environment;
        private ApplicationDbContext db;
        IPasswordHasher<User> passwordHashe;


        public UsersController(ApplicationDbContext context,
           UserManager<User> usermanager,
           IPasswordHasher<User> passwordHasher,
           IConfiguration configration,
           Microsoft.AspNetCore.Hosting.IWebHostEnvironment environment)
        {
            db = context;
            this.passwordHashe = passwordHasher;

            userManager = usermanager;
            _configration = configration;
            Environment = environment;

        }
        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromBody] LoginViewModel loginuser)
        {

            try
            {
                var user = await userManager.FindByEmailAsync(loginuser.Email);
                if (user == null)
                {
                    var loginErrorEmail = new Massage
                    {
                        massage = "Invalid Email"
                    };
                    return BadRequest(loginErrorEmail);
                }
                var pass = await userManager.CheckPasswordAsync(user, loginuser.Password);

                if (pass == false)
                {
                    var loginErrorPassword = new Massage
                    {
                        massage = "Invalid password"
                    };
                    return BadRequest(loginErrorPassword);


                }

                else if (user != null && pass != false)
                {

                    var token = new JwtService(_configration);
                    var role = await userManager.GetRolesAsync(user);



                   
                    var loginDone = new LoginReturnViewModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        UserName = user.UserName,
                        RoleName = role.FirstOrDefault(),
                        Token = token.GenerateSecurityToken(user.Email, user.Id, role.FirstOrDefault())



                    };


                    return Ok(loginDone);




                }
                return BadRequest("");
            }
            catch (Exception ex) { return BadRequest(ex.Message + "login"); }

        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registeruser)
        {

            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = registeruser.Username,
                    Email = registeruser.Email,
                   


                };
               // var user1 =db.User.FirstOrDefault(User=> User.Email == user.Email);

                IdentityResult result = await userManager.CreateAsync(user, registeruser.Password);

                if (result.Succeeded)
                {
                    var addToRole = await userManager.AddToRoleAsync(user, registeruser.RoleName);
                    if (addToRole.Succeeded)
                    {
                       
                        Massage response = new Massage
                        {
                            massage = "Susseccfull"

                        };
                        Shopping_Carts shoppingcart = new Shopping_Carts
                        {
                            UserId = user.Id,
                            Created_at = DateTime.Now
                        };
                        db.Shopping_Carts.Add(shoppingcart);
                        db.SaveChanges();

                        return Ok(response);


                    }
                    else
                    {
                        Massage response = new Massage
                        {
                            massage = "Error not added to role"

                        };
                        return Ok(response);
                    }
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {

                        Massage response = new Massage
                        {
                            massage = "Email " + registeruser.Email + " is already taken"
                            

                    };
                    return BadRequest(result.Errors);
                    }
                }
            }
            else
            {
                Massage response = new Massage
                {
                    massage = "your data is not valid"

                };

                return BadRequest(response);
            }

            return BadRequest("");
        }

    }
}
