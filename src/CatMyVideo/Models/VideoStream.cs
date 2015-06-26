using NReco.VideoConverter;
using Storage.MongoFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace CatMyVideo.Models
{
    public class VideoStream
    {
        public Stream Stream { get; set; }
        private Driver Driver;
        public VideoStream(Stream stream)
        {
            this.Stream = stream;
            this.Driver = new Driver();
        }

        public async void WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
        {
            try
            {
                outputStream = Driver.DownloadStream("0");
            }
            catch (Exception ex)
            {
                return;
            }
        }

    }
}