namespace HLeisure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialMigrate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SalesDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        SalesMasterId = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.SalesMasters", t => t.SalesMasterId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.SalesMasterId);
            
            CreateTable(
                "dbo.SalesMasters",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        timeStamp = c.String(),
                        location_name = c.String(),
                        sales_person_name = c.String(),
                        total_sales_amount = c.Double(nullable: false),
                        currency = c.String(),
                        sale_invoice_number = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        userName = c.String(),
                        password = c.String(),
                        fullName = c.String(),
                        address = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesDetails", "SalesMasterId", "dbo.SalesMasters");
            DropForeignKey("dbo.SalesDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.SalesDetails", new[] { "SalesMasterId" });
            DropIndex("dbo.SalesDetails", new[] { "ProductId" });
            DropTable("dbo.Users");
            DropTable("dbo.SalesMasters");
            DropTable("dbo.SalesDetails");
            DropTable("dbo.Products");
        }
    }
}
