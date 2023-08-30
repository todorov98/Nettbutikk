using Microsoft.EntityFrameworkCore;
using Nettbutikk.Models;
using Nettbutikk.State;
using System;

namespace Nettbutikk.Data
{
    public class WebStoreContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<CancelOrderConfirmation> CancelOrderConfirmations { get; set; }
        public DbSet<ProductOrderRelation> ProductOrderRelations { get; set; }

        public WebStoreContext(DbContextOptions<WebStoreContext> options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\WebStoreDB;Initial Catalog=WebStoreDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //Database.GetDbConnection().ConnectionString = "Data Source=(LocalDb)\\WebStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductOrderRelation>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Product>().HasData(

                new Product { Id = Guid.NewGuid(), Category = Categories.Football, Name = "UEFA Champions Leage 22/23 Final Original Edition", Description = 
                "A high quality football manufactured for the UEFA Champions Leage Final 2022/2023 season.", Price = Math.Round(102.99, 2), Currency = 
                StoreCurrency.Currency, Count = 19},

                new Product { Id = Guid.NewGuid(), Category = Categories.DrinkingBottle, Name = "Puma X2 Bottle", Description = "High quality drinking bottle" +
                "from Puma.", Price = Math.Round(22.99, 2), Currency = StoreCurrency.Currency, Count = 18},

                new Product { Id = Guid.NewGuid(), Category = Categories.Sweater, Name = "Nike Sweater XZ21 Breather Edition", Description = "A high" +
                "quality breathable sweater produced by Nike. Works well for most physical activity.", Price = Math.Round(45.99, 2), Currency = StoreCurrency
                .Currency, Count = 17},

                new Product { Id = Guid.NewGuid(), Category = Categories.FootballBoots, Name = "Nike Hypervenom Phantom ACC", Description = 
                "High quality football boots from Nike with modern ACC control that provides great control and first touch during all weather" +
                "conditions.", Price = Math.Round(249.49, 2), Count = 17, Currency = StoreCurrency.Currency},

                new Product { Id = Guid.NewGuid(), Category = Categories.Bag, Name = "Puma T23 Bag", Description = "Completely new and solid bag from" +
                "Puma.", Price = Math.Round(75.39, 2), Count = 17, Currency = StoreCurrency.Currency},

                new Product { Id = Guid.NewGuid(), Category = Categories.MountainBoots, Name = "Goretex Z34 Climber Boots", Description = 
                "High quality mountain boots from Goretex. Provides great comfort and warmth even during the harshest conditions.", Price = 
                Math.Round(167.59, 2), Count = 18, Currency = StoreCurrency.Currency},

                new Product { Id = Guid.NewGuid(), Category = Categories.SportsPants, Name = "Adidas F99 Pants Long", Description = "Nice and " +
                "comfortable sports pants by Adidas.", Price = Math.Round(34.99, 2), Count = 21, Currency = StoreCurrency.Currency},

                new Product { Id = Guid.NewGuid(), Category = Categories.Bag, Name = "Nike Elastico Bag xc21", Description = "Solid and hight " +
                "quality sports bag from Nike.", Price = Math.Round(22.99, 2), Count = 25, Currency = StoreCurrency.Currency
                }
            );
        }
    }
}