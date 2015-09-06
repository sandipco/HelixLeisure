using HLeisureClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HLeisureClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        Guid invoiceNo;
        double grandTotal;
        
        public MainWindow()
        {
            InitializeComponent();
            prepareLoad();
        }
        public UserModel curUser { get; set; }
        public MainWindow(UserModel usr)
        {
            InitializeComponent();
            curUser = usr;
            prepareLoad();
            txtLocation.Text = usr.Address;
            txtSalesPerson.Text = usr.FullName;
            txtLocation.IsReadOnly = true;
            txtSalesPerson.IsReadOnly = true;
        }
        public void prepareLoad()
        {
            
            cmbCurrency.ItemsSource = Enum.GetValues(typeof(Currency)).Cast<Currency>();
            getProductLists();
            salesDetail = new ObservableCollection<SalesDetail>();
            invoiceNo = Guid.NewGuid();
            grandTotal = 0;
            txtInvoiceNo.Text = invoiceNo.ToString();
        
        }

        public SalesDetail newSales
        {
            get { return (SalesDetail)GetValue(newSalesProperty); }
            set { SetValue(newSalesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for newSales.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty newSalesProperty =
            DependencyProperty.Register("newSales", typeof(SalesDetail), typeof(MainWindow), new PropertyMetadata(null));

        
        public ObservableCollection<SalesDetail> salesDetail
        {
            get { return (ObservableCollection<SalesDetail>)GetValue(salesDetailProperty); }
            set { SetValue(salesDetailProperty, value); }
        }

        // Using a DependencyProperty as the backing store for salesDetail.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty salesDetailProperty =
            DependencyProperty.Register("salesDetail", typeof(ObservableCollection<SalesDetail>), typeof(MainWindow), new PropertyMetadata(null));

        
        
        public Products selectedProduct
        {
            get { return (Products)GetValue(selectedProductProperty); }
            set { SetValue(selectedProductProperty, value); }
        }

        // Using a DependencyProperty as the backing store for selectedProduct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty selectedProductProperty =
            DependencyProperty.Register("selectedProduct", typeof(Products), typeof(MainWindow), new PropertyMetadata(null));

        
        private void getProductLists()
        {
            try
            {

                String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1")
                    .GetBytes(curUser.UserName + ":" + curUser.Password));
                

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:62025/");
                MediaTypeWithQualityHeaderValue hdr = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(hdr);
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);
                HttpResponseMessage response = client.GetAsync("api/Products").Result;
                if(response.StatusCode== System.Net.HttpStatusCode.Found)
                
                {
                    var products = response.Content.ReadAsAsync<IEnumerable<Products>>().Result;
                    cmbProducts.ItemsSource = products;
                    cmbProducts.DisplayMemberPath = "Name";
                    cmbProducts.SelectedValuePath = "Id";

                }
                else
                {
                    MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }
            }
            catch(System.Exception ex)
            {
                MessageBox.Show("Server Cannot be contacted!!! Please contact your system Administrator");
            }
        }

        private void cmbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selectedProduct = (Products)cmbProducts.SelectedItem;
                newSales = new SalesDetail();
                txtQuantity.Focus();
            }
            catch
            {

            }
            
            
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int q;
                if (selectedProduct == null)
                {
                    MessageBox.Show("No selection has been made");
                    cmbProducts.Focus();
                    return ;

                }
                if( String.IsNullOrWhiteSpace(txtQuantity.Text))
                {
                    MessageBox.Show("Please specify the quantity");
                    txtQuantity.Focus();
                    return ;
                }
                if(!Int32.TryParse(txtQuantity.Text,out q ))
                {
                    MessageBox.Show("Invalid Quantity");
                    txtQuantity.Focus();
                    return;
                }
                    newSales.Id = selectedProduct.Id;
                    newSales.InvoiceNo = invoiceNo;
                    newSales.Name = selectedProduct.Name;
                    newSales.UnitPrice = selectedProduct.UnitPrice;
                    newSales.Quantity = Convert.ToInt32(txtQuantity.Text);
                    newSales.Total = newSales.UnitPrice * newSales.Quantity;
                    grandTotal += newSales.Total;
                    salesDetail.Add(newSales);
                    grdSale.ItemsSource = salesDetail;
                    newSales = new SalesDetail();
                    txtQuantity.Text = null;
                    cmbProducts.SelectedItem = null;
                    selectedProduct = null;
                
            }
            catch
            {

            }
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            grandTotal = 0;
            cmbCurrency.SelectedItem = null;
            grdSale.ItemsSource = null;
            salesDetail = null;
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(cmbCurrency.Text))
                {
                    MessageBox.Show("Transaction Currency Required");
                    cmbCurrency.Focus();
                    return;
                }
                String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(curUser.UserName + ":" + curUser.Password));
                HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri("http://localhost:62025/");
                client.BaseAddress = new Uri(ServerPath.ServerPathName);
                client.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);
                FinalDataForSending data = new FinalDataForSending();
                data.LocationName = txtLocation.Text;
                data.InvoiceNo = invoiceNo;
                data.SalesPerson = txtSalesPerson.Text;
                data.TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                data.Currency = null;
                data.SalesDetails = salesDetail.ToList();
                data.Currency = cmbCurrency.Text;
                data.TotalAmount = grandTotal;

                var response = client.PostAsJsonAsync("api/Products", data).Result;

                if (response.IsSuccessStatusCode)
                {
                    var products = response.Content.ReadAsAsync<string>().Result;
                    if(products==null)
                    {
                        MessageBox.Show("Cannot Save Data");
                        return;
                    }

                    if (products.ToString() != null)
                    {
                        MessageBox.Show("Successfully Saved, your id is " + products.ToString());
                        grandTotal = 0;
                        cmbCurrency.SelectedItem = null;
                        grdSale.ItemsSource = null;
                        salesDetail = null;
                        invoiceNo = Guid.NewGuid();
                        txtInvoiceNo.Text = invoiceNo.ToString();
                    }
                    else
                        MessageBox.Show("Sorry could not save your data");

                }
                else
                {
                    MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
