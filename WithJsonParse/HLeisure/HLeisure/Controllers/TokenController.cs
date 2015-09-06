using HLeisure.Data;
using HLeisure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace HLeisure.Controllers
{
    public class TokenController : ApiController
    {
        private IHLeisureRepository _repo;

        public TokenController(IHLeisureRepository repo)
        {
            _repo = repo;
        }
        public HttpResponseMessage Post([FromBody]TokenRequestModel model)
        {
            try
            {
                var user = _repo.GetApiUsers().Where(u => u.AppId == model.ApiKey).FirstOrDefault();
                //if we actually have the user
                if (user != null)
                {
                    var secret = user.Secret;

                    // Converting the secret to the raw version
                    var key = Convert.FromBase64String(secret);
                    var provider = new System.Security.Cryptography.HMACSHA256(key);
                    // Compute Hash from API Key (NOT SECURE)
                    var hash = provider.ComputeHash(Encoding.UTF8.GetBytes(user.AppId));
                    var signature = Convert.ToBase64String(hash);
                    //checking the signature with the user
                    if (signature == model.Signature)
                    {
                        var rawTokenInfo = string.Concat(user.AppId + DateTime.UtcNow.ToString("d"));
                        var rawTokenByte = Encoding.UTF8.GetBytes(rawTokenInfo);
                        var token = provider.ComputeHash(rawTokenByte);
                        var authToken = new AuthToken()
                        {
                            Token = Convert.ToBase64String(token),
                            Expiration = DateTime.UtcNow.AddDays(15),
                            ApiUser = user
                        };
                        if (_repo.Insert(authToken) && _repo.SaveAll())
                        {
                            return Request.CreateResponse(HttpStatusCode.Created, authToken);
                        }
                    }
                }
            }
            catch 
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"ERROR");
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
