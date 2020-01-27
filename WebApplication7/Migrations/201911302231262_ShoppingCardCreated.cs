namespace WebApplication7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShoppingCardCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CardItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        Quantity = c.Int(nullable: false),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sunglasses", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CardItems", "Product_Id", "dbo.Sunglasses");
            DropIndex("dbo.CardItems", new[] { "Product_Id" });
            DropTable("dbo.CardItems");
        }
    }
}
