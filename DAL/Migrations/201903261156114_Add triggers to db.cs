namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Reflection;

    public partial class Addtriggerstodb : DbMigration
    {

        public override void Up()
        {
            // Create trigger which remove all null options, it's called after questio is deleted
            Sql("CREATE TRIGGER DELETE_OPTIONS_AFTER_DELETE_QUESTIONS ON Questions FOR DELETE AS BEGIN SET NOCOUNT ON; DELETE FROM Options WHERE QuestionId IS NULL END;");

            // Set null on OptionId and QuestionId after url stop depend on Test
            Sql("CREATE TRIGGER SET_NULL_QUESTION_OPTION_IN_ANSWERS ON URLs AFTER UPDATE AS BEGIN SET NOCOUNT ON; IF EXISTS(SELECT TestId FROM inserted) BEGIN UPDATE Answers SET OptionId = NULL, QuestionId = NULL FROM inserted as i WHERE TestResultId = i.Id END END;");
        }

        public override void Down()
        {
            Sql("IF OBJECT_ID(N'DELETE_OPTIONS_AFTER_DELETE_QUESTIONS', N'TR') IS NOT NULL DROP TRIGGER DELETE_OPTIONS_AFTER_DELETE_QUESTIONS");
            Sql("IF OBJECT_ID(N'SET_NULL_QUESTION_OPTION_IN_ANSWERS', N'TR') IS NOT NULL DROP TRIGGER SET_NULL_QUESTION_OPTION_IN_ANSWERS");
        }
    }
}
