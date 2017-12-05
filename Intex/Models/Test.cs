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
        public int OrderNumber { get; set; }
        public int SampleID { get; set; }
        public int TestTypeID { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public int EmployeeID { get; set; }

        [ForeignKey("OrderNumber")]
        public virtual WorkOrder WorkOrder { get; set; }
        [ForeignKey("SampleID")]
        public virtual Sample Sample { get; set; }
        [ForeignKey("TestTypeID")]
        public virtual TestType TestType { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }
    }
}