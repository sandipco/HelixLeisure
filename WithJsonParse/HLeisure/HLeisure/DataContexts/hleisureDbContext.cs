using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HLeisure.Data
{
    public class hleisureDbContext:DbContext
    {
        public hleisureDbContext()
            : base("dbHLeisure")
        {

        }
        public DbSet<Products> products { get; set; }
        public DbSet<SalesMaster> salesMaster { get; set; }
        public DbSet<SalesDetail> salesDetail { get; set; }
        public DbSet<Users> users { get; set; }
    }
    
}