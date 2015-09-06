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
        public string LocationName { get; set; }
        public string SalesPerson { get; set; }
        public string TimeStamp { get; set; }
        public string Currency { get; set; }
        public List<SalesDetail> SalesDetails { get; set; }
        public Guid InvoiceNo { get; set; }
        public double TotalAmount { get; set; }
    }
}
