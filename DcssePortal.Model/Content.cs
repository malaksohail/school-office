using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcssePortal.Model
{
   public class Content
    {
        public int ID { get; set; }
        public string ContentUrl { get; set; }
        public DateTime Date { get; set; }
        public string ContentTitle { get; set; }
        public virtual Course Course { get; set; }
    }
}
