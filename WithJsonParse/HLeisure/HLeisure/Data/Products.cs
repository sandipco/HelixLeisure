using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HLeisure.Data
{
    public class Products
    {
        public Products()
        {
            salesDetail = new List<SalesDetail>();   
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        public virtual ICollection<SalesDetail> salesDetail { get; set; }

    }
}
