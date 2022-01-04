using DcssePortal.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;


namespace DcssePortal.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      //modelBuilder.Entity<Enrollment>().HasOptional(x => x.Feedback).WithRequired(x => x.Enrollment);
      //modelBuilder.Entity<Enrollment>().HasOptional(x => x.Result).WithRequired(x => x.Enrollment);
      modelBuilder.Entity<Student>().Property(x => x.DOB).HasColumnType("datetime2");
      modelBuilder.Entity<Noticeboard>().Property(x => x.Date).HasColumnType("datetime2");
      modelBuilder.Entity<DateSheet>().Property(x => x.Date).HasColumnType("datetime2");
      modelBuilder.Entity<CoursesScheme>().Property(x => x.Date).HasColumnType("datetime2");
      modelBuilder.Entity<Feedback>().Property(x => x.Date).HasColumnType("datetime2");
      //modelBuilder.Entity<Feedback>().HasRequired(x => x.Enrollment);
      //modelBuilder.Entity<Enrollment>().HasOptional(x => x.Feedback);
      //modelBuilder.Entity<Feedback>().has
      base.OnModelCreating(modelBuilder);
    }
    public System.Data.Entity.DbSet<DcssePortal.Model.Student> Students { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.Complaints> Complaints { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.Content> Contents { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.CoursesScheme> CoursesSchemes { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.DateSheet> DateSheets { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.Noticeboard> Noticeboards { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.Enrollment> Enrollments { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.Faculty> Faculties { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.Feedback> Feedbacks { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.Result> Results { get; set; }
        public System.Data.Entity.DbSet<DcssePortal.Model.Admin> Admins { get; set; }
    }
}