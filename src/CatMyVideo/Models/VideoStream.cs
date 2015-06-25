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
                var buffer = new byte[65536];

                using (var video = new BinaryReader(Stream)) 
                {
                    var length = (int) video.BaseStream.Length;
                    var bytesRead = 1;

                    while (length > 0 && bytesRead > 0)
                    {
                        bytesRead = video.Read(buffer, 0, buffer.Length);
                        await outputStream.WriteAsync(buffer, 0, bytesRead);
                        length -= bytesRead;
                    }
                }
            }
            catch (HttpException ex)
            {
                return;
            }
            finally
            {
                outputStream.Close();
            }
        }

    }
}