namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateTypeChangedForFeedback : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tFeedback", "Date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tFeedback", "Date", c => c.DateTime(nullable: false));
        }
    }
}
