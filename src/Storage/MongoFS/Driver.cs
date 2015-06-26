using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        public void UploadFile(string fileName)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Open))
                {
                    var gridFsInfo = gridFS.Upload(fileName);
                    var fileId = gridFsInfo.Id;
                }

            }
            catch (MongoConnectionException e)
            {
                DriverException exception = new DriverException(e.Message, e);
                exception.ExplainProblem();
            }
        }

        public void UploadStream(Stream stream, string identifier)
        {
            try
            {
                var gridFsInfo = gridFS.Upload(stream, identifier);
            }
            catch (MongoConnectionException e)
            {
                DriverException exception = new DriverException(e.Message, e);
                exception.ExplainProblem();
            }
        }

        public Stream DownloadStream(string identifier)
        {
            var f = gridFS.FindOne(Query.EQ("filename", "0"));
            return f.OpenRead();
        }

        public void CleanAll()
        {
            this.database.Drop();
        }

        public void GetFileAndSaveTo(string inputFile, string outputFile)
        {
            try
            {
                var file = gridFS.FindOne(Query.EQ("filename", inputFile));
                using (var stream = file.OpenRead())
                {
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                    using (var newFs = new FileStream(outputFile, FileMode.Create))
                    {
                        newFs.Write(bytes, 0, bytes.Length);
                    }
                }
            }
            catch (MongoConnectionException e)
            {
                DriverException exception = new DriverException(e.Message, e);
                exception.ExplainProblem();
            }
        }

        public byte[] GetFileBytes(string inputFile, string outputFile)
        {
            try
            {
                var file = gridFS.FindOne(Query.EQ("filename", inputFile));
                using (var stream = file.OpenRead())
                {
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                    return bytes;
                }
            }
            catch (MongoConnectionException e)
            {
                DriverException exception = new DriverException(e.Message, e);
                exception.ExplainProblem();
                return null;
            }
        }

        public List<MongoGridFSFileInfo> ListFiles()
        {
            return gridFS.FindAll().ToList();
        }
    }
}
