using System;
using Data_Access_Layer.Repository.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data_Access_Layer
{
    public partial class FlyBuyDbContext : DbContext
    {
        public FlyBuyDbContext()
        {
                
        }

        public FlyBuyDbContext(DbContextOptions<FlyBuyDbContext> options)
            : base(options)
        {
        }

        public  DbSet<User> Users { get; set; }
        public  DbSet<Product> Products { get; set; }
        public  DbSet<Order> Orders { get; set; }
        public  DbSet<Bill> Bills { get; set; }
        public  DbSet<Shipping> ShippingInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=FlyBuyDB;Persist Security Info=True;User ID=root;Password=root");
            }
        }
    }
}
