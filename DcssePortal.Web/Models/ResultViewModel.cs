using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DcssePortal.Web.Models
{
  public class ResultViewModel
  {
    public int ID { get; set; }
    public int Student { get; set; }
    public int Course { get; set; }
    public short Internal { get; set; }
    public short External { get; set; }
  }
}