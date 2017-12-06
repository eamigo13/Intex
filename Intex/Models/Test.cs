using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("Test")]
    public class Test
    {
        [Key]
        public int TestID { get; set; }
        public int TestTypeID { get; set; }
        [ForeignKey("WorkOrderLine"), Column(Order = 0)]
        public int OrderNumber { get; set; }
        [ForeignKey("WorkOrderLine"), Column(Order = 1)]
        public int OrderLine { get; set; }
        public decimal TestCost { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public int EmployeeID { get; set; }

        public virtual WorkOrderLine WorkOrderLine { get; set; }

        [ForeignKey("TestTypeID")]
        public virtual TestType TestType { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }
    }
}