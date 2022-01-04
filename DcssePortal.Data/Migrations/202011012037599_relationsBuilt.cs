namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationsBuilt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tResult", "Course_ID", "dbo.tCourseContent");
            DropForeignKey("dbo.tResult", "Student_ID", "dbo.tStudent");
            DropForeignKey("dbo.tEnrollment", "Result_ID", "dbo.tResult");
            DropIndex("dbo.tEnrollment", new[] { "Result_ID" });
            DropIndex("dbo.tResult", new[] { "Course_ID" });
            DropIndex("dbo.tResult", new[] { "Student_ID" });
            //DropColumn("dbo.tResult", "ID");
            //RenameColumn(table: "dbo.tResult", name: "Result_ID", newName: "ID");
            //DropPrimaryKey("dbo.tResult");
            //DropPrimaryKey("dbo.tFeedback");
            AddColumn("dbo.tComplaints", "Student_ID", c => c.Int());
            AddColumn("dbo.tCoursesScheme", "Admin_Id", c => c.Int());
            AddColumn("dbo.tDateSheet", "Admin_Id", c => c.Int());
            AddColumn("dbo.tFeedback", "Student_ID", c => c.Int());
            AddColumn("dbo.tNoticeboard", "Admin_Id", c => c.Int());
            AlterColumn("dbo.tResult", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.tFeedback", "ID", c => c.Int(nullable: false));
            //AddPrimaryKey("dbo.tResult", "ID");
            //AddPrimaryKey("dbo.tFeedback", "ID");
            CreateIndex("dbo.tCoursesScheme", "Admin_Id");
            CreateIndex("dbo.tDateSheet", "Admin_Id");
            CreateIndex("dbo.tNoticeboard", "Admin_Id");
            CreateIndex("dbo.tComplaints", "Student_ID");
            CreateIndex("dbo.tFeedback", "ID");
            CreateIndex("dbo.tFeedback", "Student_ID");
            CreateIndex("dbo.tResult", "ID");
            AddForeignKey("dbo.tCoursesScheme", "Admin_Id", "dbo.Admins", "Id");
            AddForeignKey("dbo.tDateSheet", "Admin_Id", "dbo.Admins", "Id");
            AddForeignKey("dbo.tNoticeboard", "Admin_Id", "dbo.Admins", "Id");
            AddForeignKey("dbo.tComplaints", "Student_ID", "dbo.tStudent", "ID");
            AddForeignKey("dbo.tFeedback", "ID", "dbo.tEnrollment", "ID");
            AddForeignKey("dbo.tFeedback", "Student_ID", "dbo.tStudent", "ID");
            DropColumn("dbo.tEnrollment", "Result_ID");
            DropColumn("dbo.tResult", "Course_ID");
            DropColumn("dbo.tResult", "Student_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tResult", "Student_ID", c => c.Int());
            AddColumn("dbo.tResult", "Course_ID", c => c.Int());
            AddColumn("dbo.tEnrollment", "Result_ID", c => c.Int());
            DropForeignKey("dbo.tFeedback", "Student_ID", "dbo.tStudent");
            DropForeignKey("dbo.tFeedback", "ID", "dbo.tEnrollment");
            DropForeignKey("dbo.tComplaints", "Student_ID", "dbo.tStudent");
            DropForeignKey("dbo.tNoticeboard", "Admin_Id", "dbo.Admins");
            DropForeignKey("dbo.tDateSheet", "Admin_Id", "dbo.Admins");
            DropForeignKey("dbo.tCoursesScheme", "Admin_Id", "dbo.Admins");
            DropIndex("dbo.tResult", new[] { "ID" });
            DropIndex("dbo.tFeedback", new[] { "Student_ID" });
            DropIndex("dbo.tFeedback", new[] { "ID" });
            DropIndex("dbo.tComplaints", new[] { "Student_ID" });
            DropIndex("dbo.tNoticeboard", new[] { "Admin_Id" });
            DropIndex("dbo.tDateSheet", new[] { "Admin_Id" });
            DropIndex("dbo.tCoursesScheme", new[] { "Admin_Id" });
            DropPrimaryKey("dbo.tFeedback");
            DropPrimaryKey("dbo.tResult");
            AlterColumn("dbo.tFeedback", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.tResult", "ID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.tNoticeboard", "Admin_Id");
            DropColumn("dbo.tFeedback", "Student_ID");
            DropColumn("dbo.tDateSheet", "Admin_Id");
            DropColumn("dbo.tCoursesScheme", "Admin_Id");
            DropColumn("dbo.tComplaints", "Student_ID");
            AddPrimaryKey("dbo.tFeedback", "ID");
            AddPrimaryKey("dbo.tResult", "ID");
            RenameColumn(table: "dbo.tResult", name: "ID", newName: "Result_ID");
            AddColumn("dbo.tResult", "ID", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.tResult", "Student_ID");
            CreateIndex("dbo.tResult", "Course_ID");
            CreateIndex("dbo.tEnrollment", "Result_ID");
            AddForeignKey("dbo.tEnrollment", "Result_ID", "dbo.tResult", "ID");
            AddForeignKey("dbo.tResult", "Student_ID", "dbo.tStudent", "ID");
            AddForeignKey("dbo.tResult", "Course_ID", "dbo.tCourseContent", "ID");
        }
    }
}
