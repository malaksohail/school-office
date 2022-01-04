using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcssePortal.Model
{
    [Table("tComplaints")]
    public class Complaints
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public virtual Student Student { get; set; }
  }
}
