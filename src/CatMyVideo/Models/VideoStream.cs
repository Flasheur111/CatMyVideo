using NReco.VideoConverter;
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
        public VideoStream(Stream stream)
        {
            this.Stream = stream;
        }

        public async void WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
        {
            try
            {

            }
            catch (Exception ex)
            {
                return;
            }
        }

    }
}