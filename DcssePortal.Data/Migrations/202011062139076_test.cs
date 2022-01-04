namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tCourseContent", newName: "tCourse");
            DropForeignKey("dbo.tFeedback", "Enrollment_ID", "dbo.tEnrollment");
            DropForeignKey("dbo.tResult", "EnrollmentId", "dbo.tEnrollment");
            DropIndex("dbo.tFeedback", new[] { "Enrollment_ID" });
            DropIndex("dbo.tResult", new[] { "EnrollmentId" });
            AddColumn("dbo.tCoursesScheme", "Title", c => c.String());
            AddColumn("dbo.tCoursesScheme", "FileUrl", c => c.String());
            AddColumn("dbo.tCoursesScheme", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.tDateSheet", "ContentUrl", c => c.String());
            AddColumn("dbo.tDateSheet", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.tNoticeboard", "Title", c => c.String());
            AddColumn("dbo.tNoticeboard", "Body", c => c.String());
            AddColumn("dbo.tNoticeboard", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.tStudent", "Department", c => c.String());
            AddColumn("dbo.tStudent", "PhotoUrl", c => c.String());
            AddColumn("dbo.Contents", "DataUrl", c => c.String());
            AddColumn("dbo.Contents", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.tFaculty", "Designation", c => c.String());
            AddColumn("dbo.tFaculty", "PhotoUrl", c => c.String());
            AddColumn("dbo.tFeedback", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.tFeedback", "Course_ID", c => c.Int());
            AddColumn("dbo.tResult", "Student_ID", c => c.Int());
            AlterColumn("dbo.tResult", "TotalMarks", c => c.Short(nullable: false));
            AlterColumn("dbo.tResult", "ExternalMarks", c => c.Short(nullable: false));
            CreateIndex("dbo.tFeedback", "Course_ID");
            CreateIndex("dbo.tResult", "Student_ID");
            AddForeignKey("dbo.tFeedback", "Course_ID", "dbo.tCourse", "ID");
            AddForeignKey("dbo.tResult", "Student_ID", "dbo.tStudent", "ID");
            DropColumn("dbo.tCoursesScheme", "CourseCode");
            DropColumn("dbo.tCoursesScheme", "CourseName");
            DropColumn("dbo.tCoursesScheme", "credithour");
            DropColumn("dbo.tCoursesScheme", "SemesterOffer");
            DropColumn("dbo.tDateSheet", "Content");
            DropColumn("dbo.tNoticeboard", "NewsTitle");
            DropColumn("dbo.tNoticeboard", "NewsContent");
            DropColumn("dbo.tNoticeboard", "StartDate");
            DropColumn("dbo.tNoticeboard", "EndDate");
            DropColumn("dbo.tStudent", "FatherName");
            DropColumn("dbo.tStudent", "Password");
            DropColumn("dbo.tStudent", "Phone");
            DropColumn("dbo.tStudent", "Age");
            DropColumn("dbo.tStudent", "Profile");
            DropColumn("dbo.Contents", "Data");
            DropColumn("dbo.tFaculty", "Profile");
            DropColumn("dbo.tFeedback", "Enrollment_ID");
            DropColumn("dbo.tResult", "EnrollmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tResult", "EnrollmentId", c => c.Int(nullable: false));
            AddColumn("dbo.tFeedback", "Enrollment_ID", c => c.Int());
            AddColumn("dbo.tFaculty", "Profile", c => c.String());
            AddColumn("dbo.Contents", "Data", c => c.String());
            AddColumn("dbo.tStudent", "Profile", c => c.String());
            AddColumn("dbo.tStudent", "Age", c => c.String());
            AddColumn("dbo.tStudent", "Phone", c => c.String());
            AddColumn("dbo.tStudent", "Password", c => c.String());
            AddColumn("dbo.tStudent", "FatherName", c => c.String());
            AddColumn("dbo.tNoticeboard", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tNoticeboard", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tNoticeboard", "NewsContent", c => c.String());
            AddColumn("dbo.tNoticeboard", "NewsTitle", c => c.String());
            AddColumn("dbo.tDateSheet", "Content", c => c.String());
            AddColumn("dbo.tCoursesScheme", "SemesterOffer", c => c.String());
            AddColumn("dbo.tCoursesScheme", "credithour", c => c.Int(nullable: false));
            AddColumn("dbo.tCoursesScheme", "CourseName", c => c.String());
            AddColumn("dbo.tCoursesScheme", "CourseCode", c => c.String());
            DropForeignKey("dbo.tResult", "Student_ID", "dbo.tStudent");
            DropForeignKey("dbo.tFeedback", "Course_ID", "dbo.tCourse");
            DropIndex("dbo.tResult", new[] { "Student_ID" });
            DropIndex("dbo.tFeedback", new[] { "Course_ID" });
            AlterColumn("dbo.tResult", "ExternalMarks", c => c.String());
            AlterColumn("dbo.tResult", "TotalMarks", c => c.String());
            DropColumn("dbo.tResult", "Student_ID");
            DropColumn("dbo.tFeedback", "Course_ID");
            DropColumn("dbo.tFeedback", "Date");
            DropColumn("dbo.tFaculty", "PhotoUrl");
            DropColumn("dbo.tFaculty", "Designation");
            DropColumn("dbo.Contents", "Date");
            DropColumn("dbo.Contents", "DataUrl");
            DropColumn("dbo.tStudent", "PhotoUrl");
            DropColumn("dbo.tStudent", "Department");
            DropColumn("dbo.tNoticeboard", "Date");
            DropColumn("dbo.tNoticeboard", "Body");
            DropColumn("dbo.tNoticeboard", "Title");
            DropColumn("dbo.tDateSheet", "Date");
            DropColumn("dbo.tDateSheet", "ContentUrl");
            DropColumn("dbo.tCoursesScheme", "Date");
            DropColumn("dbo.tCoursesScheme", "FileUrl");
            DropColumn("dbo.tCoursesScheme", "Title");
            CreateIndex("dbo.tResult", "EnrollmentId");
            CreateIndex("dbo.tFeedback", "Enrollment_ID");
            AddForeignKey("dbo.tResult", "EnrollmentId", "dbo.tEnrollment", "ID", cascadeDelete: true);
            AddForeignKey("dbo.tFeedback", "Enrollment_ID", "dbo.tEnrollment", "ID");
            RenameTable(name: "dbo.tCourse", newName: "tCourseContent");
        }
    }
}
