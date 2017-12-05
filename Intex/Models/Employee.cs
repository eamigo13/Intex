using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int WageTypeID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int StateID { get; set; }
        public int CountryID { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleID { get; set; }
        public int LocationID { get; set; }

        [ForeignKey("WageTypeID")]
        public virtual WageType WageType { get; set; }
        [ForeignKey("RoleID")]
        public virtual EmployeeRole EmployeeRole { get; set; }
        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }
    }
}