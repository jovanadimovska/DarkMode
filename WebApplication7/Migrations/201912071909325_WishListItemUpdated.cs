namespace WebApplication7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WishListItemUpdated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WishListItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User = c.String(),
                        SunglassesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sunglasses", t => t.SunglassesId, cascadeDelete: true)
                .Index(t => t.SunglassesId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WishListItems", "SunglassesId", "dbo.Sunglasses");
            DropIndex("dbo.WishListItems", new[] { "SunglassesId" });
            DropTable("dbo.WishListItems");
        }
    }
}
