using CatMyVideo.Models;
using Storage.MongoFS;
using Storage.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CatMyVideo.Controllers.Api
{
    public class CommentAPIController : ApiController
    {
        //
        // GET: /Comment
        public HttpResponseMessage Get(int? page = 1, int? number = 20)
        {
            var response = Request.CreateResponse();
            return response;
        }
    }
}