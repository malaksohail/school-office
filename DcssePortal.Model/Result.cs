using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcssePortal.Model
{
    [Table("tResult")]
    public class Result
    {
        [Key]
        public int ID { get; set; }
        public short InternalMarks { get; set; }
        public short ExternalMarks { get; set; }
        public short Semester { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    //[ForeignKey("Enrollment")]
    //public int EnrollmentId { get; set; }
    //public virtual Enrollment Enrollment { get; set; }


  }
}
