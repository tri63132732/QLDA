namespace _4BroShop.Migrations
{
    using _4BroShop.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using _4BroShop.Models.EFModels;
    using System.Data.Entity.Validation;

    internal sealed class Configuration : DbMigrationsConfiguration<_4BroShop.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(_4BroShop.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            //Date=DateTime.Parse("năm-tháng-ngày")
            List<Category> Categories = new List<Category>
            {
                new Category {Id = 1, Title = "Cơm", Icon = "com.png", IsActive = true},
                new Category {Id = 2, Title = "Mì", Icon = "mi.png", IsActive = true }
            };
            foreach (var category in Categories)
            {
                context.Category.AddOrUpdate(
                    c => c.Title,
                    Categories.ToArray()
                );
            }
            context.Category.AddRange(Categories);

            List<Product> Products = new List<Product>
            {
                new Product {Id = 1, Title = "Mì thanh long", Detail = "Chi tiết sản phẩm", Price = 50000, IsActive = true, IsFeature = true, IsHome = true, CategoryId = 2},
                new Product {Id = 2, Title = "Cơm xoài", Detail = "Chi tiết sản phẩm", Price = 900000,  IsActive = true, IsFeature = true, IsHome = true, CategoryId = 1},
                new Product {Id = 3,  Title = "Xôi lạnh", Detail = "Chi tiết sản phẩm", Price = 900000, IsActive = true, IsFeature = true , IsHome = true, CategoryId = 2},
                new Product {Id = 4, Title = "Kem mắm ruốc", Detail = "Chi tiết sản phẩm", Price = 900000, IsActive = true, IsFeature = true, IsHome = true, CategoryId = 2},
                new Product {Id = 5, Title = "Sữa lắc muối ớt", Detail = "Chi tiết sản phẩm", Price = 900000, IsActive = true, IsFeature = true, IsHome = true , CategoryId = 2},
                new Product {Id = 6, Title = "Heo treo nóc nhà", Detail = "Chi tiết sản phẩm", Price = 900000, IsActive = true, IsFeature = true, IsHome = true , CategoryId = 2},
                new Product {Id = 7, Title = "Đậu hũ ngàn năm", Detail = "Chi tiết sản phẩm", Price = 900000, IsActive = true, IsFeature = true, IsHome = true , CategoryId = 2},
                new Product {Id = 8, Title = "Món ăn 0", Detail = "Chi tiết sản phẩm", Price = 900000, IsActive = true, IsFeature = true, IsHome = true , CategoryId = 2},
                new Product {Id = 9, Title = "Món ăn 0", Detail = "Chi tiết sản phẩm", Price = 900000, IsActive = true, IsFeature = true, IsHome = true , CategoryId = 2}
            };

            // thêm hoặc cập nhật sản phẩm in the context
            foreach (var product in Products)
            {
                context.Product.AddOrUpdate(
                    p => p.Id,
                    product
                );
            }
            context.Product.AddRange(Products);
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
            }
        }
    }
}
