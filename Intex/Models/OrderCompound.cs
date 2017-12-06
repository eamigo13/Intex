using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("OrderCompound")]
    public class OrderCompound
    {
        public int CompoundID { get; set; }
        public int OrderNumber { get; set; }

        [ForeignKey("CompoundID")]
        public virtual Compound Compound { get; set; }
        [ForeignKey("OrderNumber")]
        public virtual WorkOrder WorkOrder { get; set; }
    }
}