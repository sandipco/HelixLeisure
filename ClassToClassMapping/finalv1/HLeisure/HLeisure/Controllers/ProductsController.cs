using HLeisure.AuthFilter;
using HLeisure.Data;
using HLeisure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HLeisure.Controllers
{
    [HLeisureAuthorizeFilter]
    public class ProductsController : ApiController
    {
        IHLeisureRepository _repo;
        
        public ProductsController(IHLeisureRepository repo)
        {
            _repo = repo;
            
        }
        
         public HttpResponseMessage Get()
        {
            
            var products= _repo.getProducts().Select(a=>new{Name= a.Name, Id= a.Id, UnitPrice=a.UnitPrice});
            if (products != null)
                return Request.CreateResponse(HttpStatusCode.Found, products);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);
            
        }
         
        public HttpResponseMessage Post([FromBody]EntryModel model)
        {
            
             //model.salesDetail.Select(a=>new SalesDetail(){  })
             
            if (model == null) 
              return  Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No data to be entered");
            string transactionId=_repo.saveTransaction(model);
            return Request.CreateResponse(HttpStatusCode.Created,transactionId);
        }
    }
}
