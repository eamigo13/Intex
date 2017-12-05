using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("State")]
    public class State
    {
        [Key]
        public int StateID { get; set; }
        public string StateAbbrev { get; set; }
        public string StateName { get; set; }
    }
}