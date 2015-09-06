namespace HLeisure.Migrations
{
    using HLeisure.AuthFilter;
    using HLeisure.Data;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HLeisure.Data.hleisureDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HLeisure.Data.hleisureDbContext context)
        {
            List<Products> products = new List<Products>{
                new Products { Name="Tea Bag", UnitPrice=5},
                new Products { Name="Ink", UnitPrice=3},
                new Products { Name="Diaries", UnitPrice=2},
                new Products { Name="Hot Chocolate Packets", UnitPrice=5},
				new Products {Name="Lays",UnitPrice=2},
				new Products {Name="Chitos",UnitPrice=1.5}
            };
            products.ForEach(p => context.products.AddOrUpdate(pName => pName.Name, p));
            context.SaveChanges();
            List<Users> users = new List<Users>()
            {
                new Users{ userName="ABC", password=encryptObjects.Encrypt("melbourne1"), fullName="Bob Pasa", address="Milan"},
                new Users{ userName="john", password=encryptObjects.Encrypt("zebra22"),fullName ="John Scott",address="Adelaide"},
                new Users{ userName="john1", password=encryptObjects.Encrypt("perth"),fullName="John Smith", address="Brisbane"},
                new Users{ userName="debra", password=encryptObjects.Encrypt("shawn"),fullName="Debra Samaya",address="Perth"}
            };
            users.ForEach(u => context.users.AddOrUpdate(uName => uName.userName, u));
            context.SaveChanges();
        }
    }
}
