namespace WebApplication7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KeyCardChange : DbMigration
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
            AlterColumn("dbo.CardItems", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.CardItems", "Id");
        }
    }
}
