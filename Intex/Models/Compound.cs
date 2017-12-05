using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("Compound")]
    public class Compound
    {
        [Key]
        public int CompoundID { get; set; }
        public string CompoundName { get; set; }
        public decimal MolecularMass { get; set; }
    }
}