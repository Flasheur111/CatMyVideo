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
        public string UploadVideo(string filename)
        {
            Console.WriteLine("Upload Video :" + filename);
            return filename;
        }
    }
}
