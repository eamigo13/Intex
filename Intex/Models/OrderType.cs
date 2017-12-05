using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("OrderType")]
    public class OrderType
    {
        [Key]
        public int OrderTypeID { get; set; }
        public string OrderTypeDesc { get; set; }
    }
}