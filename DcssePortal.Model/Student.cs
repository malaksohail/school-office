using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcssePortal.Model
{
    [Table("tStudent")]
    public class Student
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RegNo { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }
        public string Address { get; set; }
        public string Batch { get; set; }
        public string Department { get; set; }
        public DateTime DOB { get; set; }
        public string PhotoUrl { get; set; }

    public virtual ApplicationUser User { get; set; }

    public virtual List<Complaints> Complaints { get; set; }
    public virtual List<Enrollment> Enrollments { get; set; }
    //public virtual List<Feedback> Feedbacks { get; set; }
    }
}
