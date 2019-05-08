namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addconstraintsfordbentities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tests", "Admin_Id", "dbo.Admins");
            DropForeignKey("dbo.URLs", "CreatedByAdmin_Id", "dbo.Admins");
            DropForeignKey("dbo.Questions", "Test_Id", "dbo.Tests");
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Options", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Answers", "TestResult_Id", "dbo.TestResults");
            DropIndex("dbo.Tests", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Questions", new[] { "Test_Id" });
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            DropIndex("dbo.Answers", new[] { "TestResult_Id" });
            DropIndex("dbo.Options", new[] { "Question_Id" });
            DropIndex("dbo.URLs", new[] { "CreatedBy_Id" });
            AlterColumn("dbo.Tests", "CreatedBy_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Questions", "Test_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Answers", "Question_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Answers", "TestResult_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Options", "Question_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.URLs", "Identifier", c => c.String(maxLength: 450, unicode: false));
            AlterColumn("dbo.URLs", "CreatedBy_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Tests", "CreatedBy_Id");
            CreateIndex("dbo.Questions", "Test_Id");
            CreateIndex("dbo.Answers", "Question_Id");
            CreateIndex("dbo.Answers", "TestResult_Id");
            CreateIndex("dbo.Options", "Question_Id");
            CreateIndex("dbo.URLs", "Identifier", unique: true);
            CreateIndex("dbo.URLs", "CreatedBy_Id");
            AddForeignKey("dbo.Tests", "CreatedBy_Id", "dbo.Admins", "Id", cascadeDelete: true);
            AddForeignKey("dbo.URLs", "CreatedBy_Id", "dbo.Admins", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "Test_Id", "dbo.Tests", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Answers", "Question_Id", "dbo.Questions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Options", "Question_Id", "dbo.Questions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Answers", "TestResult_Id", "dbo.TestResults", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "TestResult_Id", "dbo.TestResults");
            DropForeignKey("dbo.Options", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Questions", "Test_Id", "dbo.Tests");
            DropForeignKey("dbo.URLs", "CreatedBy_Id", "dbo.Admins");
            DropForeignKey("dbo.Tests", "CreatedBy_Id", "dbo.Admins");
            DropIndex("dbo.URLs", new[] { "CreatedBy_Id" });
            DropIndex("dbo.URLs", new[] { "Identifier" });
            DropIndex("dbo.Options", new[] { "Question_Id" });
            DropIndex("dbo.Answers", new[] { "TestResult_Id" });
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            DropIndex("dbo.Questions", new[] { "Test_Id" });
            DropIndex("dbo.Tests", new[] { "CreatedBy_Id" });
            AlterColumn("dbo.URLs", "CreatedBy_Id", c => c.Int());
            AlterColumn("dbo.URLs", "Identifier", c => c.String());
            AlterColumn("dbo.Options", "Question_Id", c => c.Int());
            AlterColumn("dbo.Answers", "TestResult_Id", c => c.Int());
            AlterColumn("dbo.Answers", "Question_Id", c => c.Int());
            AlterColumn("dbo.Questions", "Test_Id", c => c.Int());
            AlterColumn("dbo.Tests", "CreatedBy_Id", c => c.Int());
            CreateIndex("dbo.URLs", "CreatedBy_Id");
            CreateIndex("dbo.Options", "Question_Id");
            CreateIndex("dbo.Answers", "TestResult_Id");
            CreateIndex("dbo.Answers", "Question_Id");
            CreateIndex("dbo.Questions", "Test_Id");
            CreateIndex("dbo.Tests", "CreatedBy_Id");
            AddForeignKey("dbo.Answers", "TestResult_Id", "dbo.TestResults", "Id");
            AddForeignKey("dbo.Options", "Question_Id", "dbo.Questions", "Id");
            AddForeignKey("dbo.Answers", "Question_Id", "dbo.Questions", "Id");
            AddForeignKey("dbo.Questions", "Test_Id", "dbo.Tests", "Id");
            AddForeignKey("dbo.URLs", "CreatedBy_Id", "dbo.Admins", "Id");
            AddForeignKey("dbo.Tests", "CreatedBy_Id", "dbo.Admins", "Id");
        }
    }
}
