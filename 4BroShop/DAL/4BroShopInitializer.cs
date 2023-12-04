using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using _4BroShop.Models;
using _4BroShop.Models.EFModels;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
namespace _4BroShop.DAL
{
    public class _4BroShopInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //Date=DateTime.Parse("năm-tháng-ngày")
            //new ProductCategory{Id = 1, Title = "Title của sản phẩm", Icon = "icon.jpg"},
            var products = new List<Product>
            {
            //new Product{Id = 0, Title = "Món ăn 0", Detail = "Chi tiết sản phẩm", Price = 900000, PriceSale = 65000, IsSale = true, IsActive = true, IsFeature = true, ProductCategoryId = 0}
            new Product{},
            new Product{},
            new Product{},
            new Product{},
            new Product{},
            new Product{},
            new Product{},
            new Product{},
            new Product{}
            };
            //Thêm các sản phẩm vào context và lưu vào database
            products.ForEach(s => context.Products.Add(s));
            context.SaveChanges();
            //khởi tạo data danh mục
            var categories = new List<Category>
            {
            //new Category{Id = 0, Title = "Tiêu đề danh mục", Description = "Đây là danh mục ...", IsActive = true, Position = 0},
            new Category{},
            new Category{}
            };
            //Thêm các danh mục vào context và lưu vào database
            categories.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();
            //khởi tạo data danh mục sản phẩm
            var productcategories = new List<ProductCategory>
            {
            //new ProductCategory{Id = 0, Title = "Category 0", Icon = "icon.jpg"},
            new ProductCategory{},
            new ProductCategory{},
            new ProductCategory{}
            };
            productcategories.ForEach(s => context.ProductCategories.Add(s));
            context.SaveChanges();
            // Thêm một số sản phẩm vào các danh mục
            //Ví dụ: Món ăn 0 nằm trong cả Category 0 và Category 1
            //productcategories[0].Products.Add(products[0]);
            //productcategories[0].Products.Add(categories[0]);
            //productcategories[0].Products.Add(categories[1]);


            // Thêm các đối tượng vào context và lưu vào database
            context.Products.AddRange(products);
            context.Categories.AddRange(categories);
            context.SaveChanges();
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

            var adminUser = new ApplicationUser { UserName = "admin", Email = "dat.nth.63cntt@ntu.edu.vn" , PhoneNumber = "0904746501", LockoutEnabled = false};
            var result = userManager.Create(adminUser, "Admin@123456" );

            if (result.Succeeded)
            {
                // Gán vai trò "admin" cho người dùng admin
                userManager.AddToRole(adminUser.Id, "Admin");
            }
        }
    }
}