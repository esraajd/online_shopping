using Microsoft.AspNetCore.Http;

namespace WebApplication5.Models
{
    public class LoginViewModel
    {
        public string Email { set; get; }
        public string Password { set; get; }

    }
    public class LoginReturnViewModel
    {
        public string Id { get; set; }
        public string Email { set; get; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string Token { get; set; }




    }
}
