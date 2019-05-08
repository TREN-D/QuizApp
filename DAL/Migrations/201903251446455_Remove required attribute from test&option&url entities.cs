namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removerequiredattributefromtestoptionurlentities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tests", "CreatedBy_Id", "dbo.Admins");
            DropForeignKey("dbo.URLs", "CreatedBy_Id", "dbo.Admins");
            DropForeignKey("dbo.Options", "Question_Id", "dbo.Questions");
            DropIndex("dbo.Tests", new[] { "CreatedById" });
            DropIndex("dbo.Options", new[] { "QuestionId" });
            DropIndex("dbo.URLs", new[] { "CreatedById" });
            AlterColumn("dbo.Tests", "CreatedById", c => c.Int());
            AlterColumn("dbo.Options", "QuestionId", c => c.Int());
            AlterColumn("dbo.URLs", "CreatedById", c => c.Int());
            CreateIndex("dbo.Tests", "CreatedById");
            CreateIndex("dbo.Options", "QuestionId");
            CreateIndex("dbo.URLs", "CreatedById");
            AddForeignKey("dbo.Tests", "CreatedById", "dbo.Admins", "Id");
            AddForeignKey("dbo.URLs", "CreatedById", "dbo.Admins", "Id");
            AddForeignKey("dbo.Options", "QuestionId", "dbo.Questions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Options", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.URLs", "CreatedById", "dbo.Admins");
            DropForeignKey("dbo.Tests", "CreatedById", "dbo.Admins");
            DropIndex("dbo.URLs", new[] { "CreatedById" });
            DropIndex("dbo.Options", new[] { "QuestionId" });
            DropIndex("dbo.Tests", new[] { "CreatedById" });
            AlterColumn("dbo.URLs", "CreatedById", c => c.Int(nullable: false));
            AlterColumn("dbo.Options", "QuestionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Tests", "CreatedById", c => c.Int(nullable: false));
            CreateIndex("dbo.URLs", "CreatedById");
            CreateIndex("dbo.Options", "QuestionId");
            CreateIndex("dbo.Tests", "CreatedById");
            AddForeignKey("dbo.Options", "Question_Id", "dbo.Questions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.URLs", "CreatedBy_Id", "dbo.Admins", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Tests", "CreatedBy_Id", "dbo.Admins", "Id", cascadeDelete: true);
        }
    }
}
