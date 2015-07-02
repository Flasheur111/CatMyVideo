using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace CatMyVideo.Controllers
{
    public class AdminAPIController : ApiController
    {
        // PUT: /AdminAPI/Role/{id}
        [ActionName("Role")]
        [HttpPost]
        public HttpResponseMessage Role(string id)
        {
            /*HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;
            JavaScriptConverter JavaScriptSerializer = new JavaScriptConverter();
            JavaScriptConverter */
            var response = Request.CreateResponse();
            return response;
        }
    }
}