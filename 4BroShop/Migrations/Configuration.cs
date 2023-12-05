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
            List<Category> categories = new List<Category>
            {
                new Category { Title = "Cơm", IsActive = true },
                new Category { Title = "Mì", IsActive = true }
            };
            foreach (var category in categories)
            {
                context.Categories.AddOrUpdate(
                    c => c.Title,
                    category
                );
            }

            List<Product> productsToAddOrUpdate = new List<Product>
            {
                new Product { Title = "Mì thanh long", Detail = "Chi tiết sản phẩm", Price = 50000, PriceSale = 60000, IsSale = true, IsActive = true, IsFeature = true },
                new Product { Title = "Cơm xoài", Detail = "Chi tiết sản phẩm", Price = 900000, PriceSale = 65000, IsSale = true, IsActive = true, IsFeature = true },
                new Product { Title = "Xôi lạnh", Detail = "Chi tiết sản phẩm", Price = 900000, PriceSale = 65000, IsSale = true, IsActive = true, IsFeature = true },
                new Product { Title = "Kem mắm ruốc", Detail = "Chi tiết sản phẩm", Price = 900000, PriceSale = 65000, IsSale = true, IsActive = true, IsFeature = true },
                new Product { Title = "Sữa lắc muối ớt", Detail = "Chi tiết sản phẩm", Price = 900000, PriceSale = 65000, IsSale = true, IsActive = true, IsFeature = true },
                new Product { Title = "Heo treo nóc nhà", Detail = "Chi tiết sản phẩm", Price = 900000, PriceSale = 65000, IsSale = true, IsActive = true, IsFeature = true },
                new Product { Title = "Đậu hũ ngàn năm", Detail = "Chi tiết sản phẩm", Price = 900000, PriceSale = 65000, IsSale = true, IsActive = true, IsFeature = true },
                new Product { Title = "Món ăn 0", Detail = "Chi tiết sản phẩm", Price = 900000, PriceSale = 65000, IsSale = true, IsActive = true, IsFeature = true },
                new Product { Title = "Món ăn 0", Detail = "Chi tiết sản phẩm", Price = 900000, PriceSale = 65000, IsSale = true, IsActive = true, IsFeature = true }
            };

            // thêm hoặc cập nhật sản phẩm in the context
            context.Products.AddOrUpdate(
                p => p.Id,
                productsToAddOrUpdate.ToArray()
            );

            //
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Tạo vai trò Admin nếu nó chưa tồn tại
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            var adminUser = new ApplicationUser { UserName = "admin", Email = "dat.nth.63cntt@ntu.edu.vn", PhoneNumber = "0904746501", LockoutEnabled = false };
            var result = userManager.Create(adminUser, "Admin@123456");

            if (result.Succeeded)
            {
                // Gán vai trò "admin" cho người dùng admin
                userManager.AddToRole(adminUser.Id, "Admin");
            }
            context.SaveChanges();
        }
    }
}
