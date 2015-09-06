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
    public class UsersController : ApiController
    {
        IHLeisureRepository _repo;
        
        public UsersController(IHLeisureRepository repo)
        {
            _repo = repo;
            
        }
        
         public HttpResponseMessage Post([FromBody]UserModel usr)
        {
            var user=_repo.getUsers(usr.UserName, usr.Password);
            
            if (user != null)
                return Request.CreateResponse(HttpStatusCode.OK, user);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);
            
        }
    }
}
