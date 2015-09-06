using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLeisureClient.Model
{
    
    public class SalesDetail:INotifyPropertyChanged
    {
        private int _id;
        private Guid _invoiceNo;
        
        private string _name;
        private double _unitPrice;
        private int _quantity;
        private double _total;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                raisePropertyChangedEvent("Id");
            }
        }
        public Guid InvoiceNo 
        { 
            get
            {
                return _invoiceNo;
            }
                set
                {
                    _invoiceNo = value;
                    raisePropertyChangedEvent("InvoiceNo");
                }
        }
        public string Name 
        {
            get
            {
                return _name;
            }
            set 
            {
                _name = value;
                raisePropertyChangedEvent("Name");
            }
        }

        public double UnitPrice 
        {
            get
            {
                return _unitPrice;
            }
            set
            {
                _unitPrice = value;
                raisePropertyChangedEvent("unitPrice");
            }
        }

        public int Quantity 
        { 
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
                raisePropertyChangedEvent("Quantity");
            }
        }

        public double Total { 
            get 
            { 
                return _total; 
            } 
            set 
            { 
                _total = value; raisePropertyChangedEvent("Total");
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void raisePropertyChangedEvent(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
