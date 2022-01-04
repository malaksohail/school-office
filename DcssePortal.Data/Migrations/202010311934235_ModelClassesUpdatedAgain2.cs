namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelClassesUpdatedAgain2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tResult", "Course_ID", c => c.Int());
            CreateIndex("dbo.tResult", "Course_ID");
            AddForeignKey("dbo.tResult", "Course_ID", "dbo.tCourseContent", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tResult", "Course_ID", "dbo.tCourseContent");
            DropIndex("dbo.tResult", new[] { "Course_ID" });
            DropColumn("dbo.tResult", "Course_ID");
        }
    }
}
