using CatMyVideo.Models;
using Storage.MongoFS;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace CatMyVideo.Controllers.Api
{
    public class VideoAPIController : ApiController
    {
        //
        // GET: /Video/
        public HttpResponseMessage Get(string id)
        {
            var driver = new Driver();
            var videoStream = new VideoStream(driver.DownloadStream(id));
            var response = Request.CreateResponse();
            response.Content = new PushStreamContent(videoStream.WriteToStream, new MediaTypeHeaderValue("video/mp4"));
            return response;
        }

        public HttpResponseMessage GetImage(string id)
        {
            var response = Request.CreateResponse();
            try
            {
                var driver = new Driver();
                var videoStream = new VideoStream(driver.DownloadThumbnail(id));
       
                response.Content = new PushStreamContent(videoStream.WriteToStream, new MediaTypeHeaderValue("image/jpeg"));
                return response;
            }
            catch(Exception e)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = e.Message;
                return response;
            }
        }
    }
}