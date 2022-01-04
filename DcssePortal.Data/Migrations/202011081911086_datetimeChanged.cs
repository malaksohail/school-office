namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimeChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tNoticeboard", "Date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tNoticeboard", "Date", c => c.DateTime(nullable: false));
        }
    }
}
