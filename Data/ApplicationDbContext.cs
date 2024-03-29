﻿using Assignment_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Assignment_API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    ProductId = 1,
                    Active = true,
                    ProductName = "Apple iPhone 12",
                    SKU = "3852433-fg",
                    Created = new DateTime(2022, 1, 1),
                    RetailPrice = 220000,
                    SalePrice = 220000,
                    LowestPrice = 200000
                }
                );
        }
    }
}
