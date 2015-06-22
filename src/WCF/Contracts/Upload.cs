using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.Contracts;

namespace WCF.Definition
{
    class Upload : IUpload
    {
        public void UploadVideo(RemoteFileInfo file)
        {
            Console.WriteLine("Upload Video :" + file.FileName); 
        }
    }
}
