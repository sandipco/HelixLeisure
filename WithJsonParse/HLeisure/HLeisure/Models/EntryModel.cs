using HLeisure.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLeisure.Models
{
   
    public class EntryModel
    {
        public string Id { get; set; }
        public string TimeStamp { get; set; }
        public string LocationName { get; set; }
        public string SalesPerson { get; set; }
        public List<ProductModel> Products { get; set; }
        public double TotalAmount { get; set; }
        public string Currency { get; set; }
        public string InvoiceNo { get; set; }
        
    }
}