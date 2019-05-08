namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserNamepropertytoTestResultproperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestResults", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestResults", "UserName");
        }
    }
}
