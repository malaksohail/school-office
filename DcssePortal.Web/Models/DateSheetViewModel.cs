using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DcssePortal.Web.Models
{
  public class DateSheetViewModel
  {
    public int ID { get; set; }
    public string Title { get; set; }
    public string Department { get; set; }
    public HttpPostedFileBase File { get; set; }
  }
}