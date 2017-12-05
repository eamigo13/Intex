using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("ResultFile")]
    public class ResultFile
    {
        public int TestID { get; set; }
        public int ResultsTypeID { get; set; }
        public string FileLocation { get; set; }

        [ForeignKey("TestID")]
        public virtual Test Test { get; set; }
        [ForeignKey("ResultsTypeID")]
        public virtual ResultsType ResultsType { get; set; }
    }
}