namespace EasyQuizy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GeneralQuizs", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.GeneralQuizs", new[] { "CategoryId" });
            DropIndex("dbo.Categories", new[] { "Subject_Id" });
            RenameColumn(table: "dbo.Categories", name: "Subject_Id", newName: "SubjectId");
            AlterColumn("dbo.GeneralQuizs", "CategoryId", c => c.Int());
            AlterColumn("dbo.Categories", "SubjectId", c => c.Int(nullable: false));
            CreateIndex("dbo.GeneralQuizs", "CategoryId");
            CreateIndex("dbo.Categories", "SubjectId");
            AddForeignKey("dbo.GeneralQuizs", "CategoryId", "dbo.Categories", "Id");
            AddForeignKey("dbo.Categories", "SubjectId", "dbo.Subjects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.GeneralQuizs", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "SubjectId" });
            DropIndex("dbo.GeneralQuizs", new[] { "CategoryId" });
            AlterColumn("dbo.Categories", "SubjectId", c => c.Int());
            AlterColumn("dbo.GeneralQuizs", "CategoryId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Categories", name: "SubjectId", newName: "Subject_Id");
            CreateIndex("dbo.Categories", "Subject_Id");
            CreateIndex("dbo.GeneralQuizs", "CategoryId");
            AddForeignKey("dbo.Categories", "Subject_Id", "dbo.Subjects", "Id");
            AddForeignKey("dbo.GeneralQuizs", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
