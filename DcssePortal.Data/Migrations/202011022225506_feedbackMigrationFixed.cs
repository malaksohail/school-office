namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedbackMigrationFixed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tFeedback", "Course_ID", "dbo.tCourseContent");
            DropForeignKey("dbo.tFeedback", "Student_ID", "dbo.tStudent");
            DropIndex("dbo.tFeedback", new[] { "Course_ID" });
            DropIndex("dbo.tFeedback", new[] { "Student_ID" });
            DropColumn("dbo.tFeedback", "Course_ID");
            DropColumn("dbo.tFeedback", "Student_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tFeedback", "Student_ID", c => c.Int());
            AddColumn("dbo.tFeedback", "Course_ID", c => c.Int());
            CreateIndex("dbo.tFeedback", "Student_ID");
            CreateIndex("dbo.tFeedback", "Course_ID");
            AddForeignKey("dbo.tFeedback", "Student_ID", "dbo.tStudent", "ID");
            AddForeignKey("dbo.tFeedback", "Course_ID", "dbo.tCourseContent", "ID");
        }
    }
}
