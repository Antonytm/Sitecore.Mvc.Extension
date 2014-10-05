using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Sitecore.Mvc.Extension.Helpers;

namespace Sitecore.Mvc.Extension.Example.Models
{
  public class SampleModel
  {
    [StringLength(50)]
    [Required(AllowEmptyStrings = false)]
    [DisplayName("Test")]
    public object Name { get; set; }
  }
}