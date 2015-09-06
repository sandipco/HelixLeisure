using HLeisure.AuthFilter;
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
                        salesMaster.id = salesId;
                        salesMaster.timeStamp = model.TimeStamp;
                        salesMaster.location_name = model.LocationName;
                        salesMaster.sale_invoice_number = model.InvoiceNo;
                        salesMaster.total_sales_amount = model.TotalAmount;
                        salesMaster.currency = model.Currency;
                        List<SalesDetail> sales = model.SalesDetails.Select(a => new SalesDetail() { ProductId = a.Id, SalesMasterId = salesId, Quantity = a.Quantity }).ToList();
                        sales.ForEach(s => _ctx.salesDetail.Add(s));
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
            catch
            {
                return null;
            }
        }


        public IQueryable<Users> getUsers()
        {
            return _ctx.users.AsQueryable();
        }
    }
}