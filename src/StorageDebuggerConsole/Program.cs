using Storage.MongoFS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        public static Starter starter = new Starter();
        static void Main(string[] args)
        {
            #region Handle Console Event
            handler = new ConsoleEventDelegate(ConsoleEventCallback);
            SetConsoleCtrlHandler(handler, true);
            #endregion
            #region Start starter mongod.exe
            starter.Start();
            #endregion

            string inputFile = "C:\\Users\\Flash\\Videos\\League of Legends\\lol.mp4";

            Driver storage = new Driver();
            //storage.CleanAll();
            storage.UploadFile(inputFile);
            storage.ListFiles();

            //string outputFile = "C:\\Users\\Flash\\Videos\\League of Legends\\lol2.mp4";
            //storage.GetFileAndSaveTo(inputFile, outputFile);
            Console.ReadLine();

            #region Stop starter mongod.exe
            starter.Stop();
            #endregion
        }

        #region Handle Console Event Kernel
        static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2)
            {
                starter.Stop();
            }
            return false;
        }
        static ConsoleEventDelegate handler;   // Keeps it from getting garbage collected
        // Pinvoke
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);
        #endregion
    }
}
