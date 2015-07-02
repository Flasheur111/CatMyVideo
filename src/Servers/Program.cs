using System;
using Storage.WCF;
using Storage.Worker;

namespace WCFServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker worker = new Worker();
            worker.StartWorker();
            ServerManager.RunServer();
        }
    }
}
