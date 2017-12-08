using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("Results")]
    public class Result
    {
        [Key]
        public int TestID { get; set; }
        public string PassFail { get; set; }
        public string Comments { get; set; }

        [ForeignKey("TestID")]
        public virtual Test test { get; set; }
    }
}