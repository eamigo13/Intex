using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("ResultsType")]
    public class ResultsType
    {
        [Key]
        public int ResultsTypeID { get; set; }
        public string Desc { get; set; }
    }
}