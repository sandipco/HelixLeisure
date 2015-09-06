using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLeisureClient.Model
{
    public enum Currency
    {
        USD,
        AUD,
        EUR
    };
    public class FinalDataForSending
    {
        public string id { get; set; }
        public string location_name { get; set; }
        public string sales_person_name { get; set; }
        public string timeStamp { get; set; }
        public string currency { get; set; }
        public List<ProductModel> SalesDetails { get; set; }
        public Guid sale_invoice_number { get; set; }
        public double total_sale_amount { get; set; }
    }
}
