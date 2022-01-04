﻿namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateTypeChangedForCourseScheme : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tCoursesScheme", "Date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tCoursesScheme", "Date", c => c.DateTime(nullable: false));
        }
    }
}
