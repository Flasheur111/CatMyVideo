using System;
using System.Collections.Generic;
using NReco.VideoConverter;
using System.IO;
using System.Reflection;

namespace Converter.Models
{
    public class Format
    {
        private FFMpegConverter Converter;
        public List<string> Formats { get; private set; }


        public Format()
        {
            Converter = new FFMpegConverter();
            Formats = new List<string>();

            foreach (var prop in typeof(NReco.VideoConverter.Format).GetFields())
                if (prop.FieldType == typeof(string)) Formats.Add(prop.Name);

        }

        public bool CheckFormatExist(string filename)
        {
            return Formats.FindAll(x => "." + x == Path.GetExtension(filename)).Count > 0;
        }

        public void ConvertTo(Stream input, string inputFormat, Stream output, string outputFormat, string framesize)
        {
            // ERROR Same format
            if (inputFormat == outputFormat)
                return;
            NReco.VideoConverter.ConvertLiveMediaTask task = Converter.ConvertLiveMedia(input, inputFormat, output, outputFormat, new ConvertSettings()
            {
                CustomOutputArgs = "-threads 7",
                VideoFrameSize = framesize,
                VideoFrameRate = 30
            });
            task.Start();
            task.Wait();
        }

    }
}
