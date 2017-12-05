using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("OrderSample")]
    public class OrderSample
    {
        public int SampleID { get; set; }
        public int OrderNumber { get; set; }

        [ForeignKey("SampleID")]
        public virtual Sample Sample { get; set; }
        [ForeignKey("OrderNumber")]
        public virtual WorkOrder WorkOrder { get; set; }
    }
}