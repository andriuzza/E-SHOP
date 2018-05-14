namespace E_Shop.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Small = c.String(nullable: false),
                        Large = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ManufacturerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturer", t => t.ManufacturerId, cascadeDelete: true)
                .Index(t => t.ManufacturerId);
            
            CreateTable(
                "dbo.Manufacturer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Specification",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Storage = c.Int(nullable: false),
                        OSType = c.Int(nullable: false),
                        Camera = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Specification", "Id", "dbo.Product");
            DropForeignKey("dbo.Product", "ManufacturerId", "dbo.Manufacturer");
            DropForeignKey("dbo.Image", "Id", "dbo.Product");
            DropIndex("dbo.Specification", new[] { "Id" });
            DropIndex("dbo.Product", new[] { "ManufacturerId" });
            DropIndex("dbo.Image", new[] { "Id" });
            DropTable("dbo.Specification");
            DropTable("dbo.Manufacturer");
            DropTable("dbo.Product");
            DropTable("dbo.Image");
        }
    }
}
