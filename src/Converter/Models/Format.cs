using System;
using System.Collections.Generic;
using NReco.VideoConverter;
using System.IO;

namespace Converter.Models
{
    class Format
    {
        private FFMpegConverter Converter;
        public Format()
        {
            Converter = new FFMpegConverter();
        }

        public void ConvertTo(Stream input, string inputFormat, Stream output, string outputFormat)
        {
            // ERROR Same format
            if (inputFormat == outputFormat)
                return;
            NReco.VideoConverter.ConvertLiveMediaTask task = Converter.ConvertLiveMedia(input, inputFormat, output, outputFormat, new ConvertSettings()
            {
                CustomOutputArgs = "-threads 7",
                MaxDuration = 30,
                VideoFrameSize = FrameSize.hd480,
                VideoFrameRate = 30
            });
            task.Start();
            task.Wait();
        }

    }
}
