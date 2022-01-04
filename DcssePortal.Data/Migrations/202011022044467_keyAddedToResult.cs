namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keyAddedToResult : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tResult", "Grade", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tResult", "Grade");
        }
    }
}
