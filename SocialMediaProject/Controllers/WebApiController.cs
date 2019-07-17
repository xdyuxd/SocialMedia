using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace SocialMedia.Controllers
{
    [RoutePrefix("api/client")]
    public class ClientApiController : ApiController
    {
        [HttpGet]
        [Route("")]
        public List<string> GetAllClient()
        {
            var a = new List<string>();
            a.Add("a");
            return a;
        }

        [HttpGet]
        [Route("{id}")]
        public List<string> GetClient()
        {
            var a = new List<string>();
            a.Add("a");
            return a;
        }
    }
}
