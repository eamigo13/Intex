using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("OrderCompound")]
    public class OrderCompound
    {
        [Key, Column(Order=1)]
        public int CompoundID { get; set; }
        [Key, Column(Order = 2)]
        public int OrderNumber { get; set; }

        public virtual Compound Compound { get; set; }

        public virtual WorkOrder WorkOrder { get; set; }
    }
}