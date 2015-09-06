using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HLeisure;
using HLeisure.Controllers;
using HLeisure.Tests.mocks;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using HLeisure.Models;
using System.Net;
using System.Web;
using System;
using System.IO;

namespace HLeisure.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        
        private UsersController _ctrl;
        private string path,serverPath;
        [TestInitialize]
        public void Init()
        {
            _ctrl = new UsersController(new FakeRepository());
            path=AppDomain.CurrentDomain.BaseDirectory;
            path = path.Remove(path.Length - @"bin\debug\".Length);
            path= path + "\\config\\serverPath.dat";
            using (StreamReader reader = new StreamReader(path))
            {

                serverPath = reader.ReadLine();
            }

        }
        [TestMethod]
        public void Post()
        {
            var config = new HttpConfiguration();
            var link = serverPath + "/api/Users";
            var request = new HttpRequestMessage(HttpMethod.Post, link);
            var route = config.Routes.MapHttpRoute("UserApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "Users" } });
            _ctrl.ControllerContext = new HttpControllerContext(config, routeData, request);
            _ctrl.Request = request;
            _ctrl.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            UserModel  usr=new UserModel(){ UserName="billy", Password="aries11" };
            var result = _ctrl.Post(usr);
            Assert.IsNotNull(result);
            
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);


            //var result = _ctrl.Post()
        }
    }
}
