namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialentities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestTime = c.Int(),
                        TestDescription = c.String(),
                        Admin_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.Admin_Id)
                .Index(t => t.Admin_Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionText = c.String(),
                        ScroreForAnswer = c.Int(nullable: false),
                        QuestionType = c.Int(nullable: false),
                        Test_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.Test_Id)
                .Index(t => t.Test_Id);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserAnswer = c.String(),
                        IsCorrect = c.Boolean(nullable: false),
                        Option_Id = c.Int(),
                        Question_Id = c.Int(),
                        TestResult_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Options", t => t.Option_Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .ForeignKey("dbo.TestResults", t => t.TestResult_Id)
                .Index(t => t.Option_Id)
                .Index(t => t.Question_Id)
                .Index(t => t.TestResult_Id);
            
            CreateTable(
                "dbo.Options",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OptionText = c.String(),
                        IsCorrect = c.Boolean(nullable: false),
                        Question_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .Index(t => t.Question_Id);
            
            CreateTable(
                "dbo.TestResults",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DiffScore = c.Int(nullable: false),
                        TimeTestPassed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.URLs", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.URLs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActiveFrom = c.DateTime(),
                        ActiveTill = c.DateTime(),
                        UserName = c.String(),
                        NumberOfRuns = c.Int(),
                        Identifier = c.String(),
                        UserAttempts = c.Int(nullable: false),
                        CreatedByAdmin_Id = c.Int(),
                        Test_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.CreatedByAdmin_Id)
                .ForeignKey("dbo.Tests", t => t.Test_Id)
                .Index(t => t.CreatedByAdmin_Id)
                .Index(t => t.Test_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "Test_Id", "dbo.Tests");
            DropForeignKey("dbo.TestResults", "Id", "dbo.URLs");
            DropForeignKey("dbo.URLs", "Test_Id", "dbo.Tests");
            DropForeignKey("dbo.URLs", "CreatedByAdmin_Id", "dbo.Admins");
            DropForeignKey("dbo.Answers", "TestResult_Id", "dbo.TestResults");
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Options", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Answers", "Option_Id", "dbo.Options");
            DropForeignKey("dbo.Tests", "Admin_Id", "dbo.Admins");
            DropIndex("dbo.URLs", new[] { "Test_Id" });
            DropIndex("dbo.URLs", new[] { "CreatedByAdmin_Id" });
            DropIndex("dbo.TestResults", new[] { "Id" });
            DropIndex("dbo.Options", new[] { "Question_Id" });
            DropIndex("dbo.Answers", new[] { "TestResult_Id" });
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            DropIndex("dbo.Answers", new[] { "Option_Id" });
            DropIndex("dbo.Questions", new[] { "Test_Id" });
            DropIndex("dbo.Tests", new[] { "Admin_Id" });
            DropTable("dbo.URLs");
            DropTable("dbo.TestResults");
            DropTable("dbo.Options");
            DropTable("dbo.Answers");
            DropTable("dbo.Questions");
            DropTable("dbo.Tests");
            DropTable("dbo.Admins");
        }
    }
}
