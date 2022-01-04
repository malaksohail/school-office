namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resultModelUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tResult", "Semester", c => c.Short(nullable: false));
            AddColumn("dbo.tResult", "Course_ID", c => c.Int());
            CreateIndex("dbo.tResult", "Course_ID");
            AddForeignKey("dbo.tResult", "Course_ID", "dbo.tCourse", "ID");
            DropColumn("dbo.tResult", "TotalMarks");
            DropColumn("dbo.tResult", "ObtainedMarks");
            DropColumn("dbo.tResult", "Grade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tResult", "Grade", c => c.String());
            AddColumn("dbo.tResult", "ObtainedMarks", c => c.Short(nullable: false));
            AddColumn("dbo.tResult", "TotalMarks", c => c.Short(nullable: false));
            DropForeignKey("dbo.tResult", "Course_ID", "dbo.tCourse");
            DropIndex("dbo.tResult", new[] { "Course_ID" });
            DropColumn("dbo.tResult", "Course_ID");
            DropColumn("dbo.tResult", "Semester");
        }
    }
}
