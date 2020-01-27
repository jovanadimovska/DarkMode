namespace WebApplication7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedCardItem : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CardItems");
            AlterColumn("dbo.CardItems", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CardItems", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CardItems");
            AlterColumn("dbo.CardItems", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.CardItems", "Id");
        }
    }
}
