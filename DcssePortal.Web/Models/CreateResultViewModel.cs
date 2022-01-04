using DcssePortal.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DcssePortal.Web.Models
{
    public class CreateResultViewModel
    {
        public Result Result { get; set; }
        public Enrollment Enrollment { get; set; }
        //public Course Courses { get { return Enrollments.Select(x => x.Course).ToList(); } }
        //public List<Student> Students
        //{
        //    get
        //    {
        //        if (Result.Enrollment.Course == null) return Enrollments.Select(x => x.Student).ToList();
        //        return Enrollments.Where(x => x.Course == Result.Enrollment.Course).Select(x => x.Student).ToList();
        //    }
        //}
    }
}