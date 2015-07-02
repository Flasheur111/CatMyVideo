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

        public string ConvertTo(Stream input, string inputFormat, string outputFormat, string framesize)
        {
            var inputPath = "tmpin." + inputFormat;
            var outputPath = "tmpout." + outputFormat;
            var fileStreamInput = File.Create(inputPath);
            if (inputFormat == outputFormat)
            {
                input.Seek(0, SeekOrigin.Begin);
                input.CopyTo(fileStreamInput);
                fileStreamInput.Close();
                return inputPath;

            }
            var fileStreamOutput = File.Create(outputPath);

            input.Seek(0, SeekOrigin.Begin);
            input.CopyTo(fileStreamInput);
            fileStreamInput.Close();
            
            
            converter.ConvertMedia(inputPath, inputFormat, fileStreamOutput, outputFormat, new ConvertSettings() { CustomOutputArgs = "-threads 7", VideoFrameSize = framesize });
            fileStreamOutput.Close();
            
            return outputPath;
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
