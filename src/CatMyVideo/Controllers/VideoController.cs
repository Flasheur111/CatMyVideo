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

namespace CatMyVideo.Controllers
{
    public class VideoController : ApiController
    {
        //
        // GET: /Video/
        public HttpResponseMessage GetVideo(string id)
        {
            var driver = new Driver();
            var videoStream = new VideoStream(driver.DownloadStream(id));
            var response = Request.CreateResponse();
            response.Content = new PushStreamContent(videoStream.WriteToStream);
            return response;
        }
	}
}