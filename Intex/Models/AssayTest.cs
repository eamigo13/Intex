using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("AssayTest")]
    public class AssayTest
    {
        [Key, Column(Order = 1)]
        public int TestTypeID { get; set; }
        [Key, Column(Order = 2)]
        public int AssayID { get; set; }
        
        public virtual TestType TestType { get; set; }
        public virtual Assay Assay { get; set; }
}
}