namespace EasyQuizy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_formedQuiz_manytomany_with_questions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormedQuizs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        VariantsNumber = c.Int(nullable: false),
                        QuestionsNumber = c.Int(nullable: false),
                        GenerationType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FormedQuizQuestions",
                c => new
                    {
                        FormedQuiz_Id = c.Int(nullable: false),
                        Question_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FormedQuiz_Id, t.Question_Id })
                .ForeignKey("dbo.FormedQuizs", t => t.FormedQuiz_Id, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.Question_Id, cascadeDelete: true)
                .Index(t => t.FormedQuiz_Id)
                .Index(t => t.Question_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FormedQuizQuestions", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.FormedQuizQuestions", "FormedQuiz_Id", "dbo.FormedQuizs");
            DropIndex("dbo.FormedQuizQuestions", new[] { "Question_Id" });
            DropIndex("dbo.FormedQuizQuestions", new[] { "FormedQuiz_Id" });
            DropTable("dbo.FormedQuizQuestions");
            DropTable("dbo.FormedQuizs");
        }
    }
}
