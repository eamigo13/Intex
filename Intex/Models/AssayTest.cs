using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("AssayTest")]
    public class AssayTest
    {
        public int TestTypeID { get; set; }
        public int AssayID { get; set; }

        [ForeignKey("TestTypeID")]
        public virtual TestType TestType { get; set; }
        [ForeignKey("AssayID")]
        public virtual Assay Assay { get; set; }
}
}