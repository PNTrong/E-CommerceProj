﻿namespace Training.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    internal sealed class Configuration : DbMigrationsConfiguration<Training.Data.TrainingShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Training.Data.TrainingShopDbContext context)
        {
            CreateProductCategoryExample(context);

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //  This method will be called after migrating to the latest version.

            /*********************************************************************************************************/

            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TrainingShopDbContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TrainingShopDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "training",
            //    Email = "pntrongit@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    FullName = "Training Class"
            //};

            //manager.Create(user, "123654$");

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("pntrongit@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }

        private void CreateProductCategoryExample(TrainingShopDbContext context)
        {
            if( context.ProductCategories.Count() == 0)
            {
                var listProductCategory = new List<ProductCategory>(){
                new ProductCategory() { Name = "Điện Lạnh", Alias = "dien-lanh", Status = true },
                new ProductCategory() { Name = "Viễn Thông", Alias = "Vien-thong", Status = true },
                new ProductCategory() { Name = "Đồ Gia Dụng", Alias = "do-gia-dung", Status = true },
                new ProductCategory() { Name = "Mỹ Phẩm", Alias = "my-pham", Status = true }
                };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }


        }
    }
}