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
            var fileStream = File.Create("tmpin." + inputFormat);
            input.Seek(0, SeekOrigin.Begin);
            input.CopyTo(fileStream);
            fileStream.Close();

            Converter.ConvertMedia("tmpin." + inputFormat, inputFormat, output, outputFormat, new ConvertSettings() { CustomOutputArgs = "-threads 7", VideoFrameSize = framesize });

            var fileStreamOut = File.Create("tmpout." + outputFormat);
            output.Seek(0, SeekOrigin.Begin);
            output.CopyTo(fileStreamOut);
            fileStreamOut.Close();
        }
    }
}
