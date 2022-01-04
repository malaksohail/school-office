namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimeTypeChangedForDateSheet : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tDateSheet", "Date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tDateSheet", "Date", c => c.DateTime(nullable: false));
        }
    }
}
