using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("WorkOrder")]
    public class WorkOrder
    {
        [Key]
        public int OrderNumber { get; set; }
        public int OrderTypeID { get; set; }
        public int CustomerID { get; set; }
        public DateTime QuoteDate { get; set; }
        public DateTime OrderDate { get; set; }
        public int StatusID { get; set; }
        public string Comments { get; set; }

        [ForeignKey("OrderTypeID")]
        public virtual OrderType OrderType { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("StatusID")]
        public virtual OrderStatus OrderStatus { get; set; }
    }
}