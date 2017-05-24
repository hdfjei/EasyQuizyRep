namespace EasyQuizy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_images : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "ImageData", c => c.Binary());
            AddColumn("dbo.Answers", "ImageMimeType", c => c.String());
            AddColumn("dbo.Questions", "ImageData", c => c.Binary());
            AddColumn("dbo.Questions", "ImageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "ImageMimeType");
            DropColumn("dbo.Questions", "ImageData");
            DropColumn("dbo.Answers", "ImageMimeType");
            DropColumn("dbo.Answers", "ImageData");
        }
    }
}
