namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelClassesUpdatedAgain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tResult", "Student_ID", c => c.Int());
            CreateIndex("dbo.tResult", "Student_ID");
            AddForeignKey("dbo.tResult", "Student_ID", "dbo.tStudent", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tResult", "Student_ID", "dbo.tStudent");
            DropIndex("dbo.tResult", new[] { "Student_ID" });
            DropColumn("dbo.tResult", "Student_ID");
        }
    }
}
