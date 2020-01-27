namespace WebApplication7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedCardItem2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CardItems", "User", c => c.String());
            DropColumn("dbo.CardItems", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CardItems", "UserId", c => c.String());
            DropColumn("dbo.CardItems", "User");
        }
    }
}
