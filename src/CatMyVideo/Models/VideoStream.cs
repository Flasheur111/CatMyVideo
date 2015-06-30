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
        private Stream Stream;

        public VideoStream(Stream stream)
        {
            this.Stream = stream;
        }

        public async void WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
        {
            try
            {
                Stream.Seek(0, SeekOrigin.Begin);
                var buffer = new byte[65536];
                var length = (int)Stream.Length;
                var bytesRead = 1;

                while (length > 0 && bytesRead > 0)
                {
                    bytesRead = Stream.Read(buffer, 0, Math.Min(length, buffer.Length));
                    await outputStream.WriteAsync(buffer, 0, bytesRead);
                    length -= bytesRead;
                }

            }
            catch (Exception ex)
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