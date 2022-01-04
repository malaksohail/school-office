namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enrollmentLinkedWithResultSetup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tFeedback", "ID", "dbo.tEnrollment");
            DropIndex("dbo.tFeedback", new[] { "ID" });
            DropPrimaryKey("dbo.tFeedback");
            AddColumn("dbo.tFeedback", "Enrollment_ID", c => c.Int());
            AlterColumn("dbo.tFeedback", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.tFeedback", "ID");
            CreateIndex("dbo.tFeedback", "Enrollment_ID");
            AddForeignKey("dbo.tFeedback", "Enrollment_ID", "dbo.tEnrollment", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tFeedback", "Enrollment_ID", "dbo.tEnrollment");
            DropIndex("dbo.tFeedback", new[] { "Enrollment_ID" });
            DropPrimaryKey("dbo.tFeedback");
            AlterColumn("dbo.tFeedback", "ID", c => c.Int(nullable: false));
            DropColumn("dbo.tFeedback", "Enrollment_ID");
            AddPrimaryKey("dbo.tFeedback", "ID");
            CreateIndex("dbo.tFeedback", "ID");
            AddForeignKey("dbo.tFeedback", "ID", "dbo.tEnrollment", "ID");
        }
    }
}
