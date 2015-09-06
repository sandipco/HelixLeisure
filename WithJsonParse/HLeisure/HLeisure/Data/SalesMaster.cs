using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HLeisure.Data
{
    public enum Currency
    {
        USD,
        AUD,
        EUR
    };
    public class SalesMaster
    {
        public SalesMaster()
        {
            salesDetails = new List<SalesDetail>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        //String timeStamp = GetTimestamp(DateTime.Now);
        public string TimeStamp { get; set; }
        public string LocationName { get; set; }
        public string SalesPerson { get; set; }
        public double TotalAmount { get; set; }
        public string Currency { get; set; }
        public string InvoiceNo { get; set; }
        public virtual ICollection<SalesDetail> salesDetails { get; set; }

    }
}