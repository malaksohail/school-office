namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelClassesUpdatedAgain1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tStudent", "FatherName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tStudent", "FatherName");
        }
    }
}
