using Storage.MongoFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using System.IO;
using MongoDB.Driver.GridFS;
using Engine.Dbo;
using Storage.Video;

namespace Storage.WCF.Contracts
{
    class Upload : IUpload
    {
        public Driver mongo { get; set; }
        private FormatChecker formatChecker { get; set; }
        private Converter converter { get; set; }

        public Upload()
        {
            mongo = new Driver();
            formatChecker = new FormatChecker();
            converter = new Converter();
        }
        public void UploadVideo(RemoteFileInfo header)
        {
            // Tmp File Path
            var tmp_filepath = "tmpin" + header.IdVideo + ".mp4";
            // Copy header Stream
            MemoryStream input = new MemoryStream();
            header.Stream.CopyTo(input);

            // Close header Stream
            header.Stream.Dispose();

            Engine.Dbo.Encode encodeBase = new Engine.Dbo.Encode(Engine.Dbo.Encode.Definition.None, header.InputFormat, header.IdVideo, true);
            Engine.Dbo.Encode encode480 = new Engine.Dbo.Encode(Engine.Dbo.Encode.Definition.p480, header.InputFormat, header.IdVideo, false);
            Engine.Dbo.Encode encode720 = new Engine.Dbo.Encode(Engine.Dbo.Encode.Definition.p720, header.InputFormat, header.IdVideo, false);
            Engine.Dbo.Encode encode1080 = new Engine.Dbo.Encode(Engine.Dbo.Encode.Definition.p1080, header.InputFormat, header.IdVideo, false);

            encodeBase.Id = Engine.BusinessManagement.Encode.AddEncode(encodeBase);
            encode480.Id = Engine.BusinessManagement.Encode.AddEncode(encode480);
            encode720.Id = Engine.BusinessManagement.Encode.AddEncode(encode720);
            encode1080.Id = Engine.BusinessManagement.Encode.AddEncode(encode1080);

            // Create Tmp file
            var fileStream = File.Create(tmp_filepath);
            input.Seek(0, SeekOrigin.Begin);
            input.CopyTo(fileStream);
            fileStream.Close();

            // Upload thumbnail (First Frame)
            mongo.UploadThumbnail(tmp_filepath, header.IdVideo.ToString());
            // Upload base stream 
            mongo.UploadStream(tmp_filepath, encodeBase.Id.ToString());

            // Destroy Tmp file
            File.Delete(tmp_filepath);
            
            /*
            Stream s = new MemoryStream();
            Format.ConvertTo(input, inputExtension, s, NReco.VideoConverter.Format.mp4, NReco.VideoConverter.FrameSize.hd480);
            mongo.UploadStream(s, idEncode480.ToString());
            encode480.IsEncode = true;
            Engine.BusinessManagement.Encode.UpdateEncode(encode480);

            s = new MemoryStream();
            Format.ConvertTo(input, Path.GetExtension(file.FileName).Substring(1), s, NReco.VideoConverter.Format.mp4, NReco.VideoConverter.FrameSize.hd720);
            mongo.UploadStream(s, idEncode720.ToString());
            encode720.IsEncode = true;
            Engine.BusinessManagement.Encode.UpdateEncode(encode720);

            s = new MemoryStream();
            Format.ConvertTo(input, Path.GetExtension(file.FileName).Substring(1), s, NReco.VideoConverter.Format.mp4, NReco.VideoConverter.FrameSize.hd1080);
            mongo.UploadStream(s, idEncode1080.ToString());
            encode1080.IsEncode = true;
            Engine.BusinessManagement.Encode.UpdateEncode(encode1080);
            return true;
             */
        }
    }
}
