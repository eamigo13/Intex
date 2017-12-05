using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("TestType")]
    public class TestType
    {
        [Key]
        public int TestTypeID { get; set; }
        public string TestName { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
    }
}