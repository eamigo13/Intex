using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Intex.DAL
{
    public class IntexContext : DbContext
    {
        public IntexContext() : base("IntexContext")
        {

        }

        public System.Data.Entity.DbSet<Intex.Models.Appearance> Appearances { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.Assay> Assays { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.AssayTest> AssayTests { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.Compound> Compounds { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.Country> Countries { get; set; }
        
        public System.Data.Entity.DbSet<Intex.Models.Customer> Customers { get; set; }
        
        public System.Data.Entity.DbSet<Intex.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.EmployeeRole> EmployeeRoles { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.Location> Locations { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.OrderCompound> OrderCompounds { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.OrderStatus> OrderStatus { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.OrderType> OrderTypes { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.ResultFile> ResultFiles { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.ResultsType> RestultsTypes { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.Sample> Samples { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.State> States { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.Test> Tests { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.TestType> TestTypes { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.WageType> WageTypes { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.WorkOrder> WorkOrders { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.WorkOrderLine> WorkOrderLine { get; set; }
    }
}