namespace EasyQuizy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_results_foreign_keys : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Results", "StudentId");
            CreateIndex("dbo.Results", "FormedQuizId");
            AddForeignKey("dbo.Results", "FormedQuizId", "dbo.FormedQuizs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Results", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Results", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Results", "FormedQuizId", "dbo.FormedQuizs");
            DropIndex("dbo.Results", new[] { "FormedQuizId" });
            DropIndex("dbo.Results", new[] { "StudentId" });
        }
    }
}
