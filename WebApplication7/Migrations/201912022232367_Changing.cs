namespace WebApplication7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changing : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sunglasses", "brand_Id", "dbo.Brands");
            DropForeignKey("dbo.CardItems", "Product_Id", "dbo.Sunglasses");
            DropIndex("dbo.CardItems", new[] { "Product_Id" });
            DropIndex("dbo.Sunglasses", new[] { "brand_Id" });
            RenameColumn(table: "dbo.Sunglasses", name: "brand_Id", newName: "BrandId");
            RenameColumn(table: "dbo.CardItems", name: "Product_Id", newName: "SunglassesId");
            DropPrimaryKey("dbo.CardItems");
            AlterColumn("dbo.CardItems", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.CardItems", "SunglassesId", c => c.Int(nullable: false));
            AlterColumn("dbo.Sunglasses", "BrandId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.CardItems", "Id");
            CreateIndex("dbo.CardItems", "SunglassesId");
            CreateIndex("dbo.Sunglasses", "BrandId");
            AddForeignKey("dbo.Sunglasses", "BrandId", "dbo.Brands", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CardItems", "SunglassesId", "dbo.Sunglasses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CardItems", "SunglassesId", "dbo.Sunglasses");
            DropForeignKey("dbo.Sunglasses", "BrandId", "dbo.Brands");
            DropIndex("dbo.Sunglasses", new[] { "BrandId" });
            DropIndex("dbo.CardItems", new[] { "SunglassesId" });
            DropPrimaryKey("dbo.CardItems");
            AlterColumn("dbo.Sunglasses", "BrandId", c => c.Int());
            AlterColumn("dbo.CardItems", "SunglassesId", c => c.Int());
            AlterColumn("dbo.CardItems", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.CardItems", "Id");
            RenameColumn(table: "dbo.CardItems", name: "SunglassesId", newName: "Product_Id");
            RenameColumn(table: "dbo.Sunglasses", name: "BrandId", newName: "brand_Id");
            CreateIndex("dbo.Sunglasses", "brand_Id");
            CreateIndex("dbo.CardItems", "Product_Id");
            AddForeignKey("dbo.CardItems", "Product_Id", "dbo.Sunglasses", "Id");
            AddForeignKey("dbo.Sunglasses", "brand_Id", "dbo.Brands", "Id");
        }
    }
}
