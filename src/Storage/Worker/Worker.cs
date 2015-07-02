using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Storage.Worker
{
    public class WorkerModel
    {
        public Dictionary<int, List<Engine.Dbo.Encode>> encodes { get; set; }
    }

    public class Worker
    {
        private volatile bool _running;
        private Thread _t;

        public Worker()
        {
            _t = null;
            _running = true;
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

        /*public WorkerModel ConvertToWorkerModel(List<Engine.Dbo.Encode> encodes)
        {
            WorkerModel wm = new WorkerModel();
            int idVideo = -1;
            foreach(var encode in encodes.OrderBy(x => x.Video))
            {
                if (idVideo == -1)
                    wm.encodes = new Dictionary<int,List<Engine.Dbo.Encode>>();
                if (idVideo == -1 || idVideo != encode.Video)
                {
                    idVideo = encode.Video;
                    wm.encodes.Add(idVideo, new List<Engine.Dbo.Encode>());
                }
                else
                {
                    idVideo = encode.Video;

                    List<Engine.Dbo.Encode>();
                    wm.encodes.TryGetValue()
                }
            }

            return wm;
        }*/

        private void DoWork()
        {
            while (_running)
            {
                List<Engine.Dbo.Encode> toEncode = Engine.BusinessManagement.Encode.ListNotEncode();

                Console.WriteLine(toEncode.Count);
                Thread.Sleep(20000);
            }
        }
    }
}
