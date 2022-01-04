namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datatypeUpdatedForDobStudent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tStudent", "DOB", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tStudent", "DOB", c => c.DateTime(nullable: false));
        }
    }
}
