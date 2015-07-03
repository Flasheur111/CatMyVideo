using CatMyVideo.Models;
using Storage.MongoFS;
using System;
using System.Collections.Generic;
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

        public HttpResponseMessage DeleteVideo(string id)
        {
            var response = Request.CreateResponse();
            int id_parse = -1;
            Int32.TryParse(id, out id_parse);
            if (id_parse >= 0)
            {
                try
                {
                    var driver = new Driver();
                    List<Engine.Dbo.Encode> encodes = Engine.BusinessManagement.Encode.ListEncode(id_parse, true) as List<Engine.Dbo.Encode>;
                    foreach(Engine.Dbo.Encode encode in encodes)
                    {
                        Engine.BusinessManagement.Encode.DeleteEncode(encode);
                    }
                    Engine.BusinessManagement.Video.DeleteVideo(id_parse);
                    driver.DeleteStreamAndThumb(id, encodes);
                }
                catch(Exception)
                {

                }
            }
            return response;
        }
    }
}