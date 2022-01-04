namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class coureSecretCodeAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tCourse", "SecretCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tCourse", "SecretCode");
        }
    }
}
