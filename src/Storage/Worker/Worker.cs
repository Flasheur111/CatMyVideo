using Storage.MongoFS;
using Storage.Video;
using Storage.WCF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Storage.Worker
{
    public class WorkerModel
    {
        public Dictionary<Engine.Dbo.Encode, List<Engine.Dbo.Encode>> encodes { get; set; }

        public WorkerModel()
        {
            this.encodes = new Dictionary<Engine.Dbo.Encode, List<Engine.Dbo.Encode>>();
        }
    }

    public class Worker
    {
        private volatile bool _running;
        private Thread _t;
        private Driver _driver;
        private Converter _converter;

        public Worker()
        {
            _t = null;
            _running = true;
            _driver = new Driver();
            _converter = new Converter();
        }

        public void StartWorker()
        {
            _t = new Thread(DoWork);
            _t.Start();
        }

        public void StopWorker()
        {
            if (_t.IsAlive)
            {
                _t.Abort();
                _t = null;
            }
        }

        public WorkerModel ConvertToWorkerModel(List<Engine.Dbo.Encode> encodes)
        {
            WorkerModel workerModel = new WorkerModel();

            Engine.Dbo.Encode baseEncode = null;
            List<Engine.Dbo.Encode> tmpEncodes = null;
            foreach (var encode in encodes.OrderBy(x => x.Video))
            {
                if (baseEncode != null && baseEncode.Video != encode.Video)
                    workerModel.encodes.Add(baseEncode, tmpEncodes);
                if (baseEncode == null || baseEncode.Video != encode.Video)
                {
                    baseEncode = encode;
                    tmpEncodes = new List<Engine.Dbo.Encode>();
                }
                else
                    tmpEncodes.Add(encode);
            }
            if (baseEncode != null && tmpEncodes != null)
            workerModel.encodes.Add(baseEncode, tmpEncodes);
            return workerModel;
        }

        private void DoWork()
        {
            while (_running)
            {
                List<Engine.Dbo.Encode> toEncode = Engine.BusinessManagement.Encode.ListNotEncode();
                WorkerModel wm = ConvertToWorkerModel(toEncode);
                foreach (KeyValuePair<Engine.Dbo.Encode, List<Engine.Dbo.Encode>> entry in wm.encodes)
                {
                    Engine.Dbo.Encode baseEncode = entry.Key;
                    List<Engine.Dbo.Encode> toConvert = entry.Value;

                    Stream baseStream = _driver.DownloadStream(baseEncode.Id.ToString());

                    // Create Tmp file
                    var fileStream = File.Create("tmpin" + baseEncode.Id.ToString() + "." + baseEncode.InputFormat);
                    baseStream.Seek(0, SeekOrigin.Begin);
                    baseStream.CopyTo(fileStream);
                    fileStream.Close();

                    Console.WriteLine(baseStream.Length);
                    foreach (Engine.Dbo.Encode encode in toConvert)
                    {
                        Console.Write("Converting => EncodeBase : " + baseEncode.Id);
                        Console.Write("           => EncodeDestination : " + encode.Id);
                        Console.WriteLine();

                        string quality = "";
                        switch (encode.Quality)
                        {
                            case Engine.Dbo.Encode.Definition.p480:
                                quality = NReco.VideoConverter.FrameSize.hd480;
                                break;
                            case Engine.Dbo.Encode.Definition.p720:
                                quality = NReco.VideoConverter.FrameSize.hd720;
                                break;
                            case Engine.Dbo.Encode.Definition.p1080:
                                quality = NReco.VideoConverter.FrameSize.hd1080;
                                break;
                            case Engine.Dbo.Encode.Definition.None:
                                quality = NReco.VideoConverter.FrameSize.hd480;
                                break;
                            default:
                                quality = NReco.VideoConverter.FrameSize.hd480;
                                break;
                        }
                        string outpath = _converter.ConvertTo(baseStream, baseEncode.InputFormat, NReco.VideoConverter.Format.mp4, quality);
                        _driver.UploadStream(outpath, encode.Id.ToString());

                        Console.Write("Upload Convert !");

                        encode.IsEncode = true;
                        Engine.BusinessManagement.Encode.UpdateEncode(encode);
                    }
                }
                Console.WriteLine(toEncode.Count);
                Thread.Sleep(20000);
            }
        }
    }
}
