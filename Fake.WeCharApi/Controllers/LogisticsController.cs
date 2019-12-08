using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fake.WeCharApi.Controllers
{
    public class LogisticsController : ApiController
    {
        public string PostEorderservice()
        {

           string content=   base.Request.Content.ReadAsStringAsync().Result;
            return "ok";
        }
    }
}
