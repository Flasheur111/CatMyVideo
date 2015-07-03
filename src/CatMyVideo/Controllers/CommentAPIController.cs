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
using System.Web.Helpers;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Text;

namespace CatMyVideo.Controllers.Api
{
    public class CommentAPIController : ApiController
    {
        //
        // GET: /Comment
        public HttpResponseMessage Get(int videoid, int page)
        {
            var response = Request.CreateResponse();
            try
            {
                var comments = Engine.BusinessManagement.Comment.ListCommentByVideoId(videoid, 20, page);
                response.Content = new StringContent(Json.Encode(comments), Encoding.UTF8, "text/html");
                return response;
            }
            catch(Exception e)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = e.Message;
                return response;
            }
        }

        public HttpResponseMessage Delete()
        {
            var response = Request.CreateResponse();
            return response;
        }
    }
}