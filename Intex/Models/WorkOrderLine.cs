using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Intex.Models
{
    [Table("WorkOrderLine")]
    public class WorkOrderLine
    {
        [Key, Column(Order = 0)]
        public int OrderNumber { get; set; }
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderLine { get; set; }
        public int SampleID { get; set; }
        public int AssayID { get; set; }
        public decimal LineCost { get; set; }

        [ForeignKey("OrderNumber")]
        public virtual WorkOrder WorkOrder { get; set; }
        [ForeignKey("SampleID")]
        public virtual Sample Sample { get; set; }
        [ForeignKey("AssayID")]
        public virtual Assay Assay { get; set; }

    }
}