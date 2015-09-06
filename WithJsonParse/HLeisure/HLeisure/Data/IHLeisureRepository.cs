using HLeisure.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLeisure.Data
{
    public interface IHLeisureRepository
    {
        IQueryable<Products> getProducts();
        IQueryable<Users> getUsers();
        string saveTransaction(EntryModel model);
        Users getUsers(string usrName, string password);
        int getProductId(string prodName);
        bool saveTransaction(JToken model);
    }
}
