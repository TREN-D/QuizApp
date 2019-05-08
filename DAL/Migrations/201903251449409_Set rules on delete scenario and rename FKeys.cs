namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SetrulesondeletescenarioandrenameFKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "Test_Id", "dbo.Tests");
            AddForeignKey("dbo.Questions", "TestId", "dbo.Tests", "Id", cascadeDelete: true);

            DropForeignKey("dbo.Answers", "TestResult_Id", "dbo.TestResults");
            AddForeignKey("dbo.Answers", "TestResultId", "dbo.TestResults", "Id", cascadeDelete: true);

            DropForeignKey("[dbo].[URLs]", "FK_dbo.URLs_dbo.Tests_Test_Id");
            Sql("ALTER TABLE [dbo].[URLs] ADD CONSTRAINT [FK_dbo.URLs_dbo.Tests_TestId] FOREIGN KEY([TestId]) REFERENCES [dbo].[Tests]([Id]) ON DELETE SET NULL");

            DropForeignKey("[dbo].[URLs]", "FK_dbo.URLs_dbo.Admins_CreatedById");
            Sql("ALTER TABLE [dbo].[URLs] ADD CONSTRAINT [FK_dbo.URLs_dbo.Admins_CreatedById] FOREIGN KEY([CreatedById]) REFERENCES [dbo].[Admins]([Id]) ON DELETE SET NULL");

            DropForeignKey("[dbo].[Tests]", "FK_dbo.Tests_dbo.Admins_CreatedById");
            Sql("ALTER TABLE[dbo].[Tests] ADD CONSTRAINT[FK_dbo.Tests_dbo.Admins_CreatedById] FOREIGN KEY([CreatedById]) REFERENCES [dbo].[Admins]([Id]) ON DELETE SET NULL");

            DropForeignKey("[dbo].[TestResults]", "FK_dbo.TestResults_dbo.URLs_Id");
            Sql("ALTER TABLE [dbo].[TestResults] ADD CONSTRAINT [FK_dbo.TestResults_dbo.URLsId] FOREIGN KEY([Id]) REFERENCES [dbo].[URLs]([Id]) ON DELETE CASCADE");

            DropForeignKey("[dbo].[Options]", "FK_dbo.Options_dbo.Questions_QuestionId");
            Sql("ALTER TABLE [dbo].[Options] ADD CONSTRAINT [FK_dbo.Options_dbo.Questions_QuestionId] FOREIGN KEY([QuestionId]) REFERENCES [dbo].[Questions]([Id]) ON DELETE SET NULL");

            DropForeignKey("[dbo].[Answers]", "FK_dbo.Answers_dbo.Questions_Question_Id");
            Sql("ALTER TABLE[dbo].[Answers] ADD CONSTRAINT [FK_dbo.Answers_dbo.Questions_QuestionId] FOREIGN KEY([QuestionId]) REFERENCES [dbo].[Questions]([Id]) ON DELETE SET NULL");

            DropForeignKey("[dbo].[Answers]", "FK_dbo.Answers_dbo.Options_Option_Id");
            Sql("ALTER TABLE[dbo].[Answers] ADD CONSTRAINT [FK_dbo.Answers_dbo.Options_OptionId] FOREIGN KEY([OptionId]) REFERENCES [dbo].[Options]([Id]) ON DELETE SET NULL");
        }

        public override void Down()
        {
        }
    }
}
