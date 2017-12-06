using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("ResultFile")]
    public class ResultFile
    {
        [Key, Column(Order = 1)]
        public int TestID { get; set; }
        [Key, Column(Order = 2)]
        public int ResultsTypeID { get; set; }
        public string FileLocation { get; set; }

        public virtual Test Test { get; set; }
        public virtual ResultsType ResultsType { get; set; }
    }
}