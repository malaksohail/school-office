namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contents", "ContentUrl", c => c.String());
            DropColumn("dbo.Contents", "DataUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contents", "DataUrl", c => c.String());
            DropColumn("dbo.Contents", "ContentUrl");
        }
    }
}
