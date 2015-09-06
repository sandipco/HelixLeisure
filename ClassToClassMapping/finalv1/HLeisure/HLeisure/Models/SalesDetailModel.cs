using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLeisure.Models
{
    public class SalesDetailModel
    {
        public int Id { get; set; }
        public Guid InvoiceNo { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
    }
}