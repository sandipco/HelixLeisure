using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLeisure.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
    }
}