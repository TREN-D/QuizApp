namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removeunusedpropertyinurlentity : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.URLs", "UserAttempts");
        }
        
        public override void Down()
        {
            AddColumn("dbo.URLs", "UserAttempts", c => c.Int(nullable: false));
        }
    }
}
