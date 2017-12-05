using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("Appearance")]
    public class Appearance
    {
        [Key]
        public int AppearanceID { get; set; }
        public string AppearanceDesc { get; set; }

    }
}