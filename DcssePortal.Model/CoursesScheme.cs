using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcssePortal.Model
{
    [Table("tCoursesScheme")]
    public class CoursesScheme
    {
        [Key]
        public int ID { get; set; }
    
        public string Title { get; set; }
        public string FileUrl { get; set; }
        public DateTime Date { get; set; }
        public string Department { get; set; }
    }
}
