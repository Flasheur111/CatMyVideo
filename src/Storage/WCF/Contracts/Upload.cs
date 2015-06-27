using Storage.MongoFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using System.IO;
using MongoDB.Driver.GridFS;
using Converter.Models;

namespace Storage.WCF.Contracts
{
    class Upload : IUpload
    {
        public Driver mongo { get; set; }
        public Format Format { get; set; }

        public Upload()
        {
            mongo = new Driver();
            Format = new Format();
        }
        public void UploadVideo(RemoteFileInfo file)
        {
            if (Format.CheckFormatExist(Path.GetExtension(file.FileName)))
            {
                Engine.Dbo.Video video = new Engine.Dbo.Video()
                {
                    UploadDate = DateTime.Now,
                    ViewCount = 0,
                    Description = "",
                    Title = "",
                    User = 1
                };

                int idVideo = Engine.BusinessManagement.Video.AddVideo(video);

                Engine.Dbo.Encode encode480 = new Engine.Dbo.Encode()
                {
                    IsEncode = false,
                    Quality = Engine.Dbo.Encode.Definition.p480,
                    Video = idVideo
                };

                Engine.Dbo.Encode encode720 = new Engine.Dbo.Encode()
                {
                    IsEncode = false,
                    Quality = Engine.Dbo.Encode.Definition.p720,
                    Video = idVideo
                };

                Engine.Dbo.Encode encode1080 = new Engine.Dbo.Encode()
                {
                    IsEncode = false,
                    Quality = Engine.Dbo.Encode.Definition.p1080,
                    Video = idVideo
                };

                int idEncode480 = Engine.BusinessManagement.Encode.AddEncode(encode480);
                encode480.Id = idEncode480;
                int idEncode720 = Engine.BusinessManagement.Encode.AddEncode(encode720);
                encode720.Id = idEncode720;
                int idEncode1080 = Engine.BusinessManagement.Encode.AddEncode(encode1080);
                encode1080.Id = idEncode1080;

                Format f = new Format();
                Stream s = new MemoryStream();
                f.ConvertTo(file.Stream, Path.GetExtension(file.FileName).Substring(1), s, NReco.VideoConverter.Format.mp4, NReco.VideoConverter.FrameSize.hd480);
                mongo.UploadStream(file.Stream, idEncode480.ToString());
                encode480.IsEncode = true;
                Engine.BusinessManagement.Encode.UpdateEncode(encode480);

                s = new MemoryStream();
                f.ConvertTo(file.Stream, Path.GetExtension(file.FileName).Substring(1), s, NReco.VideoConverter.Format.mp4, NReco.VideoConverter.FrameSize.hd720);
                mongo.UploadStream(file.Stream, idEncode720.ToString());
                encode720.IsEncode = true;
                Engine.BusinessManagement.Encode.UpdateEncode(encode720);

                s = new MemoryStream();
                f.ConvertTo(file.Stream, Path.GetExtension(file.FileName).Substring(1), s, NReco.VideoConverter.Format.mp4, NReco.VideoConverter.FrameSize.hd1080);
                mongo.UploadStream(file.Stream, idEncode1080.ToString());
                encode1080.IsEncode = true;
                Engine.BusinessManagement.Encode.UpdateEncode(encode1080);
            }
        }
    }
}
