namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changeentitiesforbetterselfdescribing : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Tests", name: "Admin_Id", newName: "CreatedBy_Id");
            RenameColumn(table: "dbo.URLs", name: "CreatedByAdmin_Id", newName: "CreatedBy_Id");
            RenameIndex(table: "dbo.Tests", name: "IX_Admin_Id", newName: "IX_CreatedBy_Id");
            RenameIndex(table: "dbo.URLs", name: "IX_CreatedByAdmin_Id", newName: "IX_CreatedBy_Id");
            AddColumn("dbo.Questions", "ScoreForAnswer", c => c.Int(nullable: false));
            DropColumn("dbo.Questions", "ScroreForAnswer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "ScroreForAnswer", c => c.Int(nullable: false));
            DropColumn("dbo.Questions", "ScoreForAnswer");
            RenameIndex(table: "dbo.URLs", name: "IX_CreatedBy_Id", newName: "IX_CreatedByAdmin_Id");
            RenameIndex(table: "dbo.Tests", name: "IX_CreatedBy_Id", newName: "IX_Admin_Id");
            RenameColumn(table: "dbo.URLs", name: "CreatedBy_Id", newName: "CreatedByAdmin_Id");
            RenameColumn(table: "dbo.Tests", name: "CreatedBy_Id", newName: "Admin_Id");
        }
    }
}
