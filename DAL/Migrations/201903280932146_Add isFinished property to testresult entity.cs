namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddisFinishedpropertytotestresultentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestResults", "IsFinished", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestResults", "IsFinished");
        }
    }
}
