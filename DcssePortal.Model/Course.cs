using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcssePortal.Model
{
  [Table("tCourse")]
  public class Course
  {
    [Key]
    public int ID { get; set; }
    public string CourseCode { get; set; }
    public short CreditHour { get; set; }
    public string CourseTitle { get; set; }
    public string SecretCode { get; set; }
    public virtual Faculty Faculty { get; set; }
    public virtual List<Feedback> Feedbacks { get; set; }
    public virtual List<Enrollment> Enrollments { get; set; }
    public virtual List<Content> Contents { get; set; }
  }
}
