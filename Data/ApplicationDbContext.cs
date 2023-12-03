using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication5.Models;

namespace WebApplication5.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<WebApplication5.Models.RoleModelView> RoleModelView { get; set; }
        public DbSet<Orders> Orders{ get; set; }
        public DbSet<Shopping_Carts> Shopping_Carts { get; set; }
        public DbSet<Cart_Items> Cart_Items { get; set; }

        public DbSet<FirstBank> FirstBank { get; set; }
        public DbSet<SecondBank> SecondBank { get; set; }
        public DbSet<ThirdBank> ThirdBank { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<User>()
                .HasOne(u => u.Shopping_Carts)
                .WithOne(i => i.Customer)
                .HasForeignKey<Shopping_Carts>(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}
