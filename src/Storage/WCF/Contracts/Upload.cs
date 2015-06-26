using Storage.MongoFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using System.IO;
using MongoDB.Driver.GridFS;

namespace Storage.WCF.Contracts
{
    class Upload : IUpload
    {
        public Driver mongo { get; set; }

        public Upload()
        {
            mongo = new Driver();
        }
        public void UploadVideo(RemoteFileInfo file)
        {
            List<MongoGridFSFileInfo> info = mongo.ListFiles();
            mongo.UploadStream(file.Stream, "0");
            //Engine.Dbo.Video video = new Engine.Dbo.Video() { Description = "Test", Title = "Test", UploadDate = DateTime.Now, ViewCount = 0, Encodes = null, User = 1 };
            //Engine.BusinessManagement.Video.AddVideo(video);
        }
    }
}
