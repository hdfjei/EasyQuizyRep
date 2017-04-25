namespace EasyQuizy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        IsTrue = c.Boolean(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        GeneralQuizId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GeneralQuizs", t => t.GeneralQuizId, cascadeDelete: true)
                .Index(t => t.GeneralQuizId);
            
            CreateTable(
                "dbo.GeneralQuizs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CategoryId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Subject_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .Index(t => t.Subject_Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "GeneralQuizId", "dbo.GeneralQuizs");
            DropForeignKey("dbo.GeneralQuizs", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.GeneralQuizs", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Categories", new[] { "Subject_Id" });
            DropIndex("dbo.GeneralQuizs", new[] { "SubjectId" });
            DropIndex("dbo.GeneralQuizs", new[] { "CategoryId" });
            DropIndex("dbo.Questions", new[] { "GeneralQuizId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.Subjects");
            DropTable("dbo.Categories");
            DropTable("dbo.GeneralQuizs");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
