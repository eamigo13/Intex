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

        public System.Data.Entity.DbSet<Intex.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.Country> Countries { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.State> States { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.WorkOrder> WorkOrders { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.OrderStatus> OrderStatus { get; set; }

        public System.Data.Entity.DbSet<Intex.Models.OrderType> OrderTypes { get; set; }
    }
}