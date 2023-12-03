using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApplication5.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication5.Jwt
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services,
    IConfiguration config)
        {
            var secret = config.GetSection("JwtSettings").GetSection("secret").Value;

            var key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication(
            //x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}
            )
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // ValidIssuer = "localhost",
                    //ValidAudience = "localhost"
                };
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userManager = context.HttpContext.RequestServices
                        .GetRequiredService<UserManager<User>>();
                        var user = userManager.GetUserAsync(context.HttpContext.User);
                        if (user == null) context.Fail("Unauthorized");
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
            });

            return services;
        }

    }
}
