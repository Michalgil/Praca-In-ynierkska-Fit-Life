using FitLife.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitLife.Data
{
    public class AplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationUserRole, string>
    { 
    
        // zmienilem przy identityDbcontext<IdentityUser,IdentityRole,string> na swoje wlasne
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .ToTable("Product");
            modelBuilder.Entity<Product>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Product>()
                .HasOne(c => c.ProductCategory)
                .WithMany(pc => pc.Products)
                .HasForeignKey(c => c.ProductCategoryId);

            modelBuilder.Entity<ProductCategory>()
                .ToTable("ProductCategory");
            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => pc.Id);


            modelBuilder.Entity<Meal>()
                .ToTable("Meal");
            modelBuilder.Entity<Meal>()
                .HasKey(m => m.Id);
            modelBuilder.Entity<Meal>()
                .HasOne(m => m.Diet)
                .WithMany(d => d.Meals)
                .HasForeignKey(m => m.DietId);

            modelBuilder.Entity<Diet>()
                .ToTable("Diet");
            modelBuilder.Entity<ProductCategory>()
                .HasKey(d => d.Id);

            ///dodac usera do tego



        }
    }
}
