using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcssePortal.Model
{
  public  class Admin
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual List<Noticeboard> Noticeboards { get; set; }
    public virtual  List<DateSheet> DateSheets { get; set; }
    public virtual List<CoursesScheme> CoursesSchemes { get; set; }
    
  }
}
