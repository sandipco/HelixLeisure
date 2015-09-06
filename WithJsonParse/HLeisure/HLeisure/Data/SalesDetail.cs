using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLeisure.Data
{
    public class SalesDetail
    {
        public long Id { get; set; }
        public int ProductId { get; set; }
        public string SalesMasterId { get; set; }
        public int Quantity { get; set; }
        public virtual Products product { get; set; }
        public virtual SalesMaster salesMaster { get; set; }
    }
}