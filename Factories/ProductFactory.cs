using Nettbutikk.Models;
using Nettbutikk.State;
using System;
using System.Collections.Generic;

namespace Nettbutikk.Factories
{
    public class ProductFactory
    {
        public List<Product> CreateProducts()
        {
            List<Product> products = new List<Product>();

            var p = new Product()
            {
                Id = Guid.NewGuid(),
                Category = Categories.Football,
                Name = "UEFA Champions Leage 22/23 Final Original Edition",
                Description =
                    "A high quality football manufactured for the UEFA Champions Leage Final 2022/2023 season.",
                Price = Math.Round(102.99, 2),
                Currency =
                    StoreCurrency.Currency,
                Count = 1
            };

            products.Add(p);

            p = new Product()
            {
                Id = Guid.NewGuid(),
                Category = Categories.DrinkingBottle,
                Name = "Puma X2 Bottle",
                Description = "High quality drinking bottle" +
                "from Puma.",
                Price = Math.Round(22.99, 2),
                Currency = StoreCurrency.Currency,
                Count = 1
            };

            products.Add(p);

            p = new Product()
            {
                Id = Guid.NewGuid(),
                Category = Categories.Sweater,
                Name = "Nike Sweater XZ21 Breather Edition",
                Description = "A high" +
                 "quality breathable sweater produced by Nike. Works well for most physical activity.",
                Price = Math.Round(45.99, 2),
                Currency = StoreCurrency
                 .Currency,
                Count = 1
            };

            products.Add(p);

            p = new Product()
            {
                Id = Guid.NewGuid(),
                Category = Categories.FootballBoots,
                Name = "Nike Hypervenom Phantom ACC",
                Description =
                "High quality football boots from Nike with modern ACC control that provides great control and first touch during all weather" +
                "conditions.",
                Price = Math.Round(249.49, 2),
                Count = 1,
                Currency = StoreCurrency.Currency
            };

            products.Add(p);

            p = new Product()
            {
                Id = Guid.NewGuid(),
                Category = Categories.Bag,
                Name = "Puma T23 Bag",
                Description = "Completely new and solid bag from" +
                "Puma.",
                Price = Math.Round(75.39, 2),
                Count = 1,
                Currency = StoreCurrency.Currency
            };

            products.Add(p);

            p = new Product()
            {
                Id = Guid.NewGuid(),
                Category = Categories.MountainBoots,
                Name = "Goretex Z34 Climber Boots",
                Description =
                "High quality mountain boots from Goretex. Provides great comfort and warmth even during the harshest conditions.",
                Price =
                Math.Round(167.59, 2),
                Count = 1,
                Currency = StoreCurrency.Currency
            };

            products.Add(p);

            p = new Product()
            {
                Id = Guid.NewGuid(),
                Category = Categories.SportsPants,
                Name = "Adidas F99 Pants Long",
                Description = "Nice and " +
                "comfortable sports pants by Adidas.",
                Price = Math.Round(34.99, 2),
                Count = 1,
                Currency = StoreCurrency.Currency
            };

            products.Add(p);

            p = new Product()
            {
                Id = Guid.NewGuid(),
                Category = Categories.Bag,
                Name = "Nike Elastico Bag xc21",
                Description = "Solid and hight " +
                "quality sports bag from Nike.",
                Price = Math.Round(22.99, 2),
                Count = 1,
                Currency = StoreCurrency.Currency
            };

            products.Add(p);

            return products;
        }
    }
}
