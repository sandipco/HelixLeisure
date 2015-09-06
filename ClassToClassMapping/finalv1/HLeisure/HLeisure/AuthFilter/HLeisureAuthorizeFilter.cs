using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Text;
using System.Security.Principal;
using System.Threading;
using Ninject;
using HLeisure.Data;

namespace HLeisure.AuthFilter
{
    public class HLeisureAuthorizeFilter : AuthorizationFilterAttribute
    {
        
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                return;
            }
            var authHeader = actionContext.Request.Headers.Authorization;
            if(authHeader!=null)
            {
                if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
  !string.IsNullOrWhiteSpace(authHeader.Parameter))
                {
                    var rawCredentials = authHeader.Parameter;
                    var encoding = Encoding.GetEncoding("iso-8859-1");
                    var credentials = encoding.GetString(Convert.FromBase64String(rawCredentials));
                    var split = credentials.Split(':');
                    var username = split[0];
                    var password = split[1];
                    if(checkLogin(username,password))
                    {
                        var principal = new GenericPrincipal(new GenericIdentity(username), null);
                        Thread.CurrentPrincipal = principal;
                        return;
                    }
                }
            }
            RespondUnAuthorize(actionContext);
        }

        private bool checkLogin(string usrName,string password)
        {
            using (var ctx = new hleisureDbContext())
            {
                string pwd=encryptObjects.Encrypt(password);
                var usr = ctx.users.Where(a => a.userName == usrName && a.password == pwd).FirstOrDefault();
                if (usr != null)
                    return true;
                else
                    return false;
                
            }

        }
        private void RespondUnAuthorize(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            
                actionContext.Response.Headers.Add("WWW-Authenticate",
                  "Basic Scheme='CountingKs' location='http://localhost:62025/account/login'");
            
        }
    }
}