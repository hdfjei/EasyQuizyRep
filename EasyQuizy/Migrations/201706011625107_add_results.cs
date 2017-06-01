namespace EasyQuizy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_results : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResultInPercent = c.Double(nullable: false),
                        StudentId = c.Int(nullable: false),
                        FormedQuizId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Results");
        }
    }
}
