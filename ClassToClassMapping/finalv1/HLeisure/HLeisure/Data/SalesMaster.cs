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
        public Guid id { get; set; }
        //String timeStamp = GetTimestamp(DateTime.Now);
        public string timeStamp { get; set; }
        public string location_name { get; set; }
        public string sales_person_name { get; set; }
        public double total_sales_amount { get; set; }
        public string currency { get; set; }
        public Guid sale_invoice_number { get; set; }
        public virtual ICollection<SalesDetail> salesDetails { get; set; }

    }
}