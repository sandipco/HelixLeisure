using HLeisure.AuthFilter;
using HLeisure.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;


namespace HLeisure.Data
{
    public class HLeisureRepository:IHLeisureRepository
    {
        hleisureDbContext _ctx;
        public HLeisureRepository(hleisureDbContext ctx)
        {
            _ctx = ctx;
        }
        public IQueryable<Products> getProducts()
        {
            return _ctx.products;
        }


        
        public string saveTransaction(Models.EntryModel model)
        {
            
                using (var transaction=_ctx.Database.BeginTransaction())
                {
                    try
                    {
                        Guid salesId = Guid.NewGuid();
                        SalesMaster salesMaster = new SalesMaster();
                        salesMaster.Id = model.Id;
                        salesMaster.TimeStamp = model.TimeStamp;
                        salesMaster.LocationName = model.LocationName;
                        salesMaster.InvoiceNo = model.InvoiceNo;
                        salesMaster.TotalAmount = model.TotalAmount;
                        salesMaster.Currency = model.Currency;
                        List<ProductModel> sales = model.Products.Select(a => new ProductModel() { Name= a.Name, Quantity=a.Quantity, SaleAmount=a.SaleAmount }).ToList();
                        foreach (var sale in sales)
                        {
                            int i = getProductId(sale.Name);
                            SalesDetail detail = new SalesDetail()
                            {
                                ProductId = i,
                                Quantity = sale.Quantity,
                                SalesMasterId = model.Id
                            };
                            _ctx.salesDetail.Add(detail);
                        }
                        
                        _ctx.salesMaster.Add(salesMaster);
                        _ctx.SaveChanges();
                        transaction.Commit();
                        return salesId.ToString();
                    }
                    catch
                    {
                        transaction.Rollback();
                        return null;
                    }
                }
            
        }





        public Users getUsers(string usrName, string password)
        {
            try
            {
                string pwd = encryptObjects.Encrypt(password);
                var usr = _ctx.users.Where(a => a.userName == usrName && a.password == pwd).FirstOrDefault();
                if (usr != null)
                    return usr;
                else
                    return null;
            }
            catch(System.Exception ex)
            {
                string s = ex.ToString();
                return null;
            }
        }


        public IQueryable<Users> getUsers()
        {
            return _ctx.users.AsQueryable();
        }


        public int getProductId(string prodName)
        {
            try
            {
                var i = _ctx.products.Where(a => a.Name == prodName).FirstOrDefault();
                return (int)i.Id;
            }
            catch
            {
                return -1;
            }
        }


        public bool saveTransaction(Newtonsoft.Json.Linq.JToken model)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    JObject obj = JObject.Parse(model.ToString());
                    JToken jsonId = obj["id"];
                    SalesMaster salesMaster = new SalesMaster();
                    salesMaster.Id = jsonId.ToString();

                    JToken jsonLocation = obj["location_name"];
                    salesMaster.LocationName = jsonLocation.ToString();


                    JToken jsonSalesPerson = obj["sales_person_name"];
                    salesMaster.SalesPerson = jsonSalesPerson.ToString();

                    JToken jsonTimeStamp = obj["timeStamp"];
                    salesMaster.TimeStamp = jsonTimeStamp.ToString();

                    JToken jsonCurrency = obj["currency"];
                    salesMaster.Currency = jsonCurrency.ToString();

                    JToken jsonSalesInvoiceNo = obj["sale_invoice_number"];
                    salesMaster.InvoiceNo = jsonSalesInvoiceNo.ToString();

                    JToken jsonSalesAmount = obj["total_sale_amount"];
                    salesMaster.TotalAmount = Convert.ToDouble(jsonSalesAmount.ToString());
                    JToken jsonSalesDetail = obj["SalesDetails"];
                    JToken[] sales = jsonSalesDetail.ToArray();
                    _ctx.salesMaster.Add(salesMaster);
                    foreach (JToken t in sales)
                    {
                        string prodName = t["name"].ToString();
                        int prodId = getProductId(prodName);
                        if (prodId < 0)
                            throw new NullReferenceException();
                        string salesId = salesMaster.Id;
                        int quantity = Convert.ToInt32(t["quantity"].ToString());
                        SalesDetail salesDetail = new SalesDetail() { ProductId = prodId, Quantity = quantity, SalesMasterId = salesId };
                        _ctx.salesDetail.Add(salesDetail);
                    }
                    _ctx.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            
        }
    }
}