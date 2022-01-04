namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enrollmentUniqueSet : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tEnrollment", "Student_ID", "dbo.tStudent");
            DropForeignKey("dbo.tEnrollment", "Course_ID", "dbo.tCourseContent");
            DropIndex("dbo.tEnrollment", new[] { "Course_ID" });
            DropIndex("dbo.tEnrollment", new[] { "Student_ID" });
            AlterColumn("dbo.tEnrollment", "Course_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.tEnrollment", "Student_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.tEnrollment", "Course_ID");
            CreateIndex("dbo.tEnrollment", "Student_ID");
            AddForeignKey("dbo.tEnrollment", "Student_ID", "dbo.tStudent", "ID", cascadeDelete: true);
            AddForeignKey("dbo.tEnrollment", "Course_ID", "dbo.tCourseContent", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tEnrollment", "Course_ID", "dbo.tCourseContent");
            DropForeignKey("dbo.tEnrollment", "Student_ID", "dbo.tStudent");
            DropIndex("dbo.tEnrollment", new[] { "Student_ID" });
            DropIndex("dbo.tEnrollment", new[] { "Course_ID" });
            AlterColumn("dbo.tEnrollment", "Student_ID", c => c.Int());
            AlterColumn("dbo.tEnrollment", "Course_ID", c => c.Int());
            CreateIndex("dbo.tEnrollment", "Student_ID");
            CreateIndex("dbo.tEnrollment", "Course_ID");
            AddForeignKey("dbo.tEnrollment", "Course_ID", "dbo.tCourseContent", "ID");
            AddForeignKey("dbo.tEnrollment", "Student_ID", "dbo.tStudent", "ID");
        }
    }
}
