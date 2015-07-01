using System;
using System.Collections.Generic;
using NReco.VideoConverter;
using System.IO;
using System.Reflection;
using System.Drawing;

namespace Storage.Video 
{
    public class Converter 
    {
        private FFMpegConverter converter;

        public Converter()
        {
            converter = new FFMpegConverter();
        }

        public void ConvertTo(Stream input, string inputFormat, Stream output, string outputFormat, string framesize)
        {
            var fileStream = File.Create("tmpin." + inputFormat);
            input.Seek(0, SeekOrigin.Begin);
            input.CopyTo(fileStream);
            fileStream.Close();

            converter.ConvertMedia("tmpin." + inputFormat, inputFormat, output, outputFormat, new ConvertSettings() { CustomOutputArgs = "-threads 7", VideoFrameSize = framesize });

            var fileStreamOut = File.Create("tmpout." + outputFormat);
            output.Seek(0, SeekOrigin.Begin);
            output.CopyTo(fileStreamOut);
            fileStreamOut.Close();
        }

        public Image GetThumbnail(string inputFile)
        {
            MemoryStream thumbnail = new MemoryStream();
            converter.GetVideoThumbnail(inputFile, thumbnail);
            var img = Bitmap.FromStream(thumbnail);
            return img;
        }
    }
}
