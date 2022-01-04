using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DcssePortal.Web.Models
{
  public class FeedbackViewModel
  {
    public int ID { get; set; }
    public string Title { get; set; }
    public int Course { get; set; }
    public HttpPostedFileBase file { get; set; }
  }
}