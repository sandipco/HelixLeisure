using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HLeisure.Data;


namespace HLeisure.Tests.mocks
{
    public class FakeRepository: IHLeisureRepository
    {
        public IQueryable<Products> getProducts()
        {
            return new Products[]
            {
                new Products()
                {
                    Id=1,
                    Name ="ABC",
                    UnitPrice=3
                },
                new Products()
                {
                    Id=2,
                    Name="PPP",
                    UnitPrice=2.5
                },
                new Products()
                {
                    Id=101,
                    Name="PPgf",
                    UnitPrice=0.5
                }
            }.AsQueryable();
        }


        public string saveTransaction(Models.EntryModel model)
        {
            return "BH763-7HBHO-TRRTNOP";
        }





       


        public Users getUsers(string usrName, string password)
        {
            return getUsers().Where(u=>u.userName==usrName && u.password==password).FirstOrDefault();
        }


        public IQueryable<Users> getUsers()
        {
            return new Users[]
            {
                new Users()
                {
                     id=101,
                     fullName ="BPN",
                     address="XYZ",
                     userName="tomato",
                     password="potato"
                },
                new Users()
                {
                     id=102,
                     fullName ="Vincent Holly",
                     address="New Castle",
                     userName="imtoogood",
                     password="amigood"
                },
                new Users()
                {
                     id=103,
                     fullName ="Shania Bott",
                     address="California",
                     userName="shania",
                     password="bottshania"
                }
            }.AsQueryable();
        }
    }
}
