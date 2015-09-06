using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Web.Http.ModelBinding.Binders;
using HLeisure.App_Start;
using HLeisure.Models;
using System.Web.Http.ModelBinding;

namespace HLeisure
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

           
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ProductsApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller="Products", id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "UserApi",
                routeTemplate: "api/Users",
                defaults: new { controller = "Users" }
            );
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            
        }
    }
}
