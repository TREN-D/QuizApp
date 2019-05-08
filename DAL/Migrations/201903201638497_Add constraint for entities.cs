namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addconstraintforentities : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Tests", name: "CreatedBy_Id", newName: "CreatedById");
            RenameColumn(table: "dbo.URLs", name: "CreatedBy_Id", newName: "CreatedById");
            RenameColumn(table: "dbo.Questions", name: "Test_Id", newName: "TestId");
            RenameColumn(table: "dbo.URLs", name: "Test_Id", newName: "TestId");
            RenameColumn(table: "dbo.Answers", name: "Question_Id", newName: "QuestionId");
            RenameColumn(table: "dbo.Options", name: "Question_Id", newName: "QuestionId");
            RenameColumn(table: "dbo.Answers", name: "Option_Id", newName: "OptionId");
            RenameColumn(table: "dbo.Answers", name: "TestResult_Id", newName: "TestResultId");
            RenameIndex(table: "dbo.Tests", name: "IX_CreatedBy_Id", newName: "IX_CreatedById");
            RenameIndex(table: "dbo.Questions", name: "IX_Test_Id", newName: "IX_TestId");
            RenameIndex(table: "dbo.Answers", name: "IX_TestResult_Id", newName: "IX_TestResultId");
            RenameIndex(table: "dbo.Answers", name: "IX_Question_Id", newName: "IX_QuestionId");
            RenameIndex(table: "dbo.Answers", name: "IX_Option_Id", newName: "IX_OptionId");
            RenameIndex(table: "dbo.Options", name: "IX_Question_Id", newName: "IX_QuestionId");
            RenameIndex(table: "dbo.URLs", name: "IX_CreatedBy_Id", newName: "IX_CreatedById");
            RenameIndex(table: "dbo.URLs", name: "IX_Test_Id", newName: "IX_TestId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.URLs", name: "IX_TestId", newName: "IX_Test_Id");
            RenameIndex(table: "dbo.URLs", name: "IX_CreatedById", newName: "IX_CreatedBy_Id");
            RenameIndex(table: "dbo.Options", name: "IX_QuestionId", newName: "IX_Question_Id");
            RenameIndex(table: "dbo.Answers", name: "IX_OptionId", newName: "IX_Option_Id");
            RenameIndex(table: "dbo.Answers", name: "IX_QuestionId", newName: "IX_Question_Id");
            RenameIndex(table: "dbo.Answers", name: "IX_TestResultId", newName: "IX_TestResult_Id");
            RenameIndex(table: "dbo.Questions", name: "IX_TestId", newName: "IX_Test_Id");
            RenameIndex(table: "dbo.Tests", name: "IX_CreatedById", newName: "IX_CreatedBy_Id");
            RenameColumn(table: "dbo.Answers", name: "TestResultId", newName: "TestResult_Id");
            RenameColumn(table: "dbo.Answers", name: "OptionId", newName: "Option_Id");
            RenameColumn(table: "dbo.Options", name: "QuestionId", newName: "Question_Id");
            RenameColumn(table: "dbo.Answers", name: "QuestionId", newName: "Question_Id");
            RenameColumn(table: "dbo.URLs", name: "TestId", newName: "Test_Id");
            RenameColumn(table: "dbo.Questions", name: "TestId", newName: "Test_Id");
            RenameColumn(table: "dbo.URLs", name: "CreatedById", newName: "CreatedBy_Id");
            RenameColumn(table: "dbo.Tests", name: "CreatedById", newName: "CreatedBy_Id");
        }
    }
}
