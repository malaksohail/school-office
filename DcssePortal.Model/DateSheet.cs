using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcssePortal.Model
{[Table("tDateSheet")]
    public class DateSheet
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string ContentUrl { get; set; }
        public DateTime Date { get; set; }
        public string Department { get; set; }
        public virtual Admin Admin { get; set; }

    }
}
