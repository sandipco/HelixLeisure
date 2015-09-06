using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLeisure.Models
{
    //[System.Web.Http.ModelBinding.ModelBinder(typeof(ProductModelBinder))]
    public class EntryModel
    {
        
        public string LocationName { get; set; }
        public string SalesPerson { get; set; }
        public string TimeStamp { get; set; }
        public string Currency { get; set; }
        public List<SalesDetailModel> SalesDetails { get; set; }
        public Guid InvoiceNo { get; set; }
        public double TotalAmount { get; set; }
    }
}