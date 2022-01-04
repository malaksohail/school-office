namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adminAddedRelationAddedToUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tFaculty", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.tStudent", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.tFaculty", "User_Id");
            CreateIndex("dbo.tStudent", "User_Id");
            AddForeignKey("dbo.tFaculty", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.tStudent", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tStudent", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.tFaculty", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.tStudent", new[] { "User_Id" });
            DropIndex("dbo.tFaculty", new[] { "User_Id" });
            DropColumn("dbo.tStudent", "User_Id");
            DropColumn("dbo.tFaculty", "User_Id");
        }
    }
}
