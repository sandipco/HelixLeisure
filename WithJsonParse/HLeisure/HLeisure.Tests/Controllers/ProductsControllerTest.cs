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
using Newtonsoft.Json.Linq;

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
            var link = serverPath + "/api/Products";
            StringBuilder json= new StringBuilder();
            json.Append("{");
            json.Append("'id': 'c7cf22cc-48cf-4e54-8188-c415f4f029f5','location_name': 'Perth',");
            json.Append("'sales_person_name': 'Debra Samaya','timeStamp': '20150906130935419','currency': 'USD',");
            json.Append("'SalesDetails': [ { 'name': 'Ink', 'quantity': 1, 'sale_amount': 3.0 },");
            json.Append(" {'name': 'Tea Bag','quantity': 2,'sale_amount': 10.0");
            json.Append(" } ],'sale_invoice_number': '3e7b0bfd-0bd8-448c-aad0-b4ebb27ef9d6', 'total_sale_amount': 13.0}");
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, link);
            var route = config.Routes.MapHttpRoute("ProductsApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "Products" } });
            _ctrl.ControllerContext = new HttpControllerContext(config, routeData, request);
            _ctrl.Request = request;
            _ctrl.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            var result = _ctrl.Post(json.ToString());
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);



        }

        
    }
}
