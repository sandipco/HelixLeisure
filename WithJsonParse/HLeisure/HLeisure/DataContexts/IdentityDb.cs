using Microsoft.AspNet.Identity.EntityFramework;
using HLeisure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLeisure.DataContexts
{
    public class IdentityDb : IdentityDbContext<ApplicationUser>
    {
        public IdentityDb()
            : base("dbHLeisure")
        {
        }
        public static IdentityDb Create()
        {
            return new IdentityDb();
        }
    }
}