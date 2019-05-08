namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addstartandfinishtimefortestresult : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestResults", "StartTest", c => c.DateTime(nullable: false));
            AddColumn("dbo.TestResults", "FinishTest", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestResults", "FinishTest");
            DropColumn("dbo.TestResults", "StartTest");
        }
    }
}
