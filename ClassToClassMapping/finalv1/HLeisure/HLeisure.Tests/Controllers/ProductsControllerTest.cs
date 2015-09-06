using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HLeisure;
using HLeisure.Controllers;
using HLeisure.Tests.mocks;
using System.Net;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using HLeisure.Models;
using HLeisure.Data;
using Newtonsoft.Json;
using System.IO;

namespace HLeisure.Tests.Controllers
{
    [TestClass]
    public class ProductsControllerTest
    {
        private string path, serverPath;
        private ProductsController _ctrl;

        [TestInitialize]
        public void Init()
        {
            _ctrl = new ProductsController(new FakeRepository());
            path = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Remove(path.Length - @"bin\debug\".Length);
            path = path + "\\config\\serverPath.dat";
            using (StreamReader reader = new StreamReader(path))
            {

                serverPath = reader.ReadLine();
            }

        }
        [TestMethod]
        public void Get()
        {
            var config = new HttpConfiguration();
            var link = serverPath + "/api/Products";
            
            var request = new HttpRequestMessage(HttpMethod.Get, serverPath);
            var route = config.Routes.MapHttpRoute("ProductsApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "Products" } });

            _ctrl.ControllerContext = new HttpControllerContext(config, routeData, request);
            _ctrl.Request = request;
            _ctrl.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;



            var results = _ctrl.Get();
            Assert.IsNotNull(results);
            Assert.AreEqual(HttpStatusCode.Found, results.StatusCode);
            
        }

        [TestMethod]
        public void Post()
        {
            Guid invNo= Guid.NewGuid();
            List<SalesDetailModel> sales= new List<SalesDetailModel>(){
                new SalesDetailModel ()
                { 
                     Id=1,
                     InvoiceNo=invNo,
                     Name="ABC",
                     Quantity=5,
                     Total=20,
                     UnitPrice=4
                },
                new SalesDetailModel ()
                { 
                    Id=2,
                     InvoiceNo=invNo,
                     Name="XYZ",
                     Quantity=3,
                     Total=9,
                     UnitPrice=3
                }
            };
            EntryModel objModel = new EntryModel();
            objModel.LocationName = "Sydney";
            objModel.SalesPerson = "Peter Brank";
            objModel.InvoiceNo = invNo;
            objModel.TimeStamp = "201506518729";
            objModel.TotalAmount= 11.23;
            objModel.Currency = "USD";
            objModel.SalesDetails= sales ;
            var link = serverPath + "/api/Products";
            
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, link);
            var route = config.Routes.MapHttpRoute("ProductsApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "Products" } });
            _ctrl.ControllerContext = new HttpControllerContext(config, routeData, request);
            _ctrl.Request = request;
            _ctrl.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            var result = _ctrl.Post(objModel);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);



        }


    }
}
