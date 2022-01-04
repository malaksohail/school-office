namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enrollmentLinkedWithResult : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tResult", "ID", "dbo.tEnrollment");
            DropIndex("dbo.tResult", new[] { "ID" });
            DropPrimaryKey("dbo.tResult");
            AddColumn("dbo.tResult", "EnrollmentId", c => c.Int(nullable: false));
            AlterColumn("dbo.tResult", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.tResult", "ID");
            CreateIndex("dbo.tResult", "EnrollmentId");
            AddForeignKey("dbo.tResult", "EnrollmentId", "dbo.tEnrollment", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tResult", "EnrollmentId", "dbo.tEnrollment");
            DropIndex("dbo.tResult", new[] { "EnrollmentId" });
            DropPrimaryKey("dbo.tResult");
            AlterColumn("dbo.tResult", "ID", c => c.Int(nullable: false));
            DropColumn("dbo.tResult", "EnrollmentId");
            AddPrimaryKey("dbo.tResult", "ID");
            CreateIndex("dbo.tResult", "ID");
            AddForeignKey("dbo.tResult", "ID", "dbo.tEnrollment", "ID");
        }
    }
}
