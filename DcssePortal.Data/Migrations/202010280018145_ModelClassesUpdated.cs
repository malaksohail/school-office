namespace DcssePortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelClassesUpdated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tComplaints",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Detail = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Data = c.String(),
                        ContentTitle = c.String(),
                        Course_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.tCourseContent", t => t.Course_ID)
                .Index(t => t.Course_ID);
            
            CreateTable(
                "dbo.tCourseContent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourseCode = c.String(),
                        CourseTitle = c.String(),
                        Faculty_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.tFaculty", t => t.Faculty_ID)
                .Index(t => t.Faculty_ID);
            
            CreateTable(
                "dbo.tFaculty",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Phone = c.String(),
                        Age = c.String(),
                        Profile = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tCoursesScheme",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourseCode = c.String(),
                        CourseName = c.String(),
                        credithour = c.Int(nullable: false),
                        SemesterOffer = c.String(),
                        Department = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tDateSheet",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        Department = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tEnrollment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Course_ID = c.Int(),
                        Student_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.tCourseContent", t => t.Course_ID)
                .ForeignKey("dbo.tStudent", t => t.Student_ID)
                .Index(t => t.Course_ID)
                .Index(t => t.Student_ID);
            
            CreateTable(
                "dbo.tStudent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Phone = c.String(),
                        Age = c.String(),
                        Profiile = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tFeedback",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        FileURL = c.String(),
                        Course_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.tCourseContent", t => t.Course_ID)
                .Index(t => t.Course_ID);
            
            CreateTable(
                "dbo.tNoticeboard",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NewsTitle = c.String(),
                        NewsContent = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tResult",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TotalMarks = c.String(),
                        ObtainedMarks = c.String(),
                        Student_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.tStudent", t => t.Student_ID)
                .Index(t => t.Student_ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.tResult", "Student_ID", "dbo.tStudent");
            DropForeignKey("dbo.tFeedback", "Course_ID", "dbo.tCourseContent");
            DropForeignKey("dbo.tEnrollment", "Student_ID", "dbo.tStudent");
            DropForeignKey("dbo.tEnrollment", "Course_ID", "dbo.tCourseContent");
            DropForeignKey("dbo.Contents", "Course_ID", "dbo.tCourseContent");
            DropForeignKey("dbo.tCourseContent", "Faculty_ID", "dbo.tFaculty");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.tResult", new[] { "Student_ID" });
            DropIndex("dbo.tFeedback", new[] { "Course_ID" });
            DropIndex("dbo.tEnrollment", new[] { "Student_ID" });
            DropIndex("dbo.tEnrollment", new[] { "Course_ID" });
            DropIndex("dbo.tCourseContent", new[] { "Faculty_ID" });
            DropIndex("dbo.Contents", new[] { "Course_ID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.tResult");
            DropTable("dbo.tNoticeboard");
            DropTable("dbo.tFeedback");
            DropTable("dbo.tStudent");
            DropTable("dbo.tEnrollment");
            DropTable("dbo.tDateSheet");
            DropTable("dbo.tCoursesScheme");
            DropTable("dbo.tFaculty");
            DropTable("dbo.tCourseContent");
            DropTable("dbo.Contents");
            DropTable("dbo.tComplaints");
        }
    }
}
