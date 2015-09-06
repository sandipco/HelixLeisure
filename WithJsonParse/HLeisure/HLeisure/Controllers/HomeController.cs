using HLeisure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HLeisure.Controllers
{
    public class HomeController : Controller
    {
        private IHLeisureRepository _repo;

        public HomeController(IHLeisureRepository repo)
        {
            _repo = repo;
        }
        public ActionResult Index()
        {
            
            ViewBag.Title = "Home Page";
            var results = _repo.getProducts();
            return View(results);
        }
    }
}
