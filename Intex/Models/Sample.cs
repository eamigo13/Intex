using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("Sample")]
    public class Sample
    {
        [Key]
        public int SampleID { get; set; }
        public int CompoundID { get; set; }
        public decimal ReportedQty { get; set; }
        public decimal MeasuredQty { get; set; }
        public decimal MTD { get; set; }
        public int AppearanceID { get; set; }

        [ForeignKey("CompoundID")]
        public virtual Compound Compound { get; set; }
        [ForeignKey("AppearanceID")]
        public virtual Appearance Appearance { get; set; }
    }
}