namespace WebApplication7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sunglasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        Name = c.String(),
                        imgUrl = c.String(),
                        sale = c.Boolean(nullable: false),
                        category = c.Int(nullable: false),
                        rating = c.Int(nullable: false),
                        brand_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.brand_Id)
                .Index(t => t.brand_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sunglasses", "brand_Id", "dbo.Brands");
            DropIndex("dbo.Sunglasses", new[] { "brand_Id" });
            DropTable("dbo.Sunglasses");
            DropTable("dbo.Brands");
        }
    }
}
