using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("WageType")]
    public class WageType
    {
        [Key]
        public int WageTypeID { get; set; }
        public string WageTypeDesc { get; set; }
    }
}