using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DcssePortal.Web.Models
{
  public class CourseSchemeViewModel
  {
    public int ID { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Department { get; set; }
    [Required]
    public HttpPostedFileBase File { get; set; }
  }
}