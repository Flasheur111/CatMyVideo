using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.MongoFS
{
    public class Driver
    {
        private MongoServer server;
        private MongoDatabase database;
        private MongoGridFS gridFS;

        public Driver()
        {
            string url = ConfigurationManager.AppSettings["mongoUrl"];
            string db = ConfigurationManager.AppSettings["mongoDb"];
            this.server = MongoServer.Create(url);

            this.database = server.GetDatabase(db);
            this.gridFS = database.GridFS;
        }

        public void UploadThumbnail(string tmp_path, string identifier)
        {
            var tmp_img_path = identifier + "-thumbnail";
            try
            {
                NReco.VideoConverter.FFMpegConverter converter = new NReco.VideoConverter.FFMpegConverter();
                converter.GetVideoThumbnail(tmp_path, tmp_img_path);
                using (var fs = new FileStream(tmp_img_path, FileMode.Open))
                {
                    var gridFsInfo = gridFS.Upload(fs, tmp_img_path);
                }
                File.Delete(tmp_img_path);

            }
            catch (MongoConnectionException e)
            {
                DriverException exception = new DriverException(e.Message, e);
                exception.ExplainProblem();
            }
        }

        public void UploadStream(string tmp_path, string identifier)
        {
            try
            {
                using (var fs = new FileStream(tmp_path, FileMode.Open))
                {
                    var gridFsInfo = gridFS.Upload(fs, identifier);
                }

            }
            catch (MongoConnectionException e)
            {
                DriverException exception = new DriverException(e.Message, e);
                exception.ExplainProblem();
            }
        }

        public Stream DownloadThumbnail(string identifier)
        {
            MemoryStream s = new MemoryStream();
            gridFS.Download(s, identifier + "-thumbnail");
            return s;
        }

        public Stream DownloadStream(string identifier)
        {
            MemoryStream s = new MemoryStream();
            gridFS.Download(s, identifier);
            return s;
        }

        public void CleanAll()
        {
            this.database.Drop();
        }

        public List<MongoGridFSFileInfo> ListFiles()
        {
            return gridFS.FindAll().ToList();
        }
    }
}
