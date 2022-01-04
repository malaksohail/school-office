namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelClassesUpdated1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tResult", "Student_ID", "dbo.tStudent");
            DropIndex("dbo.tResult", new[] { "Student_ID" });
            AddColumn("dbo.tCourseContent", "CreditHour", c => c.Short(nullable: false));
            AddColumn("dbo.tEnrollment", "Result_ID", c => c.Int());
            AddColumn("dbo.tStudent", "RegNo", c => c.String());
            AddColumn("dbo.tStudent", "Address", c => c.String());
            AddColumn("dbo.tStudent", "Batch", c => c.String());
            AddColumn("dbo.tStudent", "DOB", c => c.DateTime(nullable: false));
            AddColumn("dbo.tStudent", "Profile", c => c.String());
            AddColumn("dbo.tResult", "InternalMarks", c => c.Short(nullable: false));
            AddColumn("dbo.tResult", "ExternalMarks", c => c.String());
            AlterColumn("dbo.tResult", "ObtainedMarks", c => c.Short(nullable: false));
            CreateIndex("dbo.tEnrollment", "Result_ID");
            AddForeignKey("dbo.tEnrollment", "Result_ID", "dbo.tResult", "ID");
            DropColumn("dbo.tStudent", "Profiile");
            DropColumn("dbo.tResult", "Student_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tResult", "Student_ID", c => c.Int());
            AddColumn("dbo.tStudent", "Profiile", c => c.String());
            DropForeignKey("dbo.tEnrollment", "Result_ID", "dbo.tResult");
            DropIndex("dbo.tEnrollment", new[] { "Result_ID" });
            AlterColumn("dbo.tResult", "ObtainedMarks", c => c.String());
            DropColumn("dbo.tResult", "ExternalMarks");
            DropColumn("dbo.tResult", "InternalMarks");
            DropColumn("dbo.tStudent", "Profile");
            DropColumn("dbo.tStudent", "DOB");
            DropColumn("dbo.tStudent", "Batch");
            DropColumn("dbo.tStudent", "Address");
            DropColumn("dbo.tStudent", "RegNo");
            DropColumn("dbo.tEnrollment", "Result_ID");
            DropColumn("dbo.tCourseContent", "CreditHour");
            CreateIndex("dbo.tResult", "Student_ID");
            AddForeignKey("dbo.tResult", "Student_ID", "dbo.tStudent", "ID");
        }
    }
}
