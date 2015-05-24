using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.MongoFS
{
    public class DriverException : MongoConnectionException
    {
        public DriverException(string message, MongoConnectionException innerException)
            : base(message, innerException)
        {
        }

        public void ExplainProblem()
        {
            Console.WriteLine("Exception -> Start the mongoFS Server (mongod.exe) in MongoServer folder");
        }
    }
}
