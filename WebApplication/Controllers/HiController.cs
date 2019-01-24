using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApplication.Controllers
{
    public class HiController : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return "Hi everyone!";
        }
    }
}
