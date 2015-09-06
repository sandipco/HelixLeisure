﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLeisure.Data
{
    public class AuthToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public ApiUser ApiUser { get; set; }
    }
}