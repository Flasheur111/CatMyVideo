using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Video
{
    public class FormatChecker
    {
        public static bool CheckFormatExist(string filename)
        {
            List<string> Formats = GetFormats();
            return Formats.FindAll(x => "." + x == Path.GetExtension(filename)).Count > 0;
        }

        public static List<string> GetFormats()
        {
            List<string> Formats = new List<string>();
            foreach (var prop in typeof(NReco.VideoConverter.Format).GetFields())
                if (prop.FieldType == typeof(string)) Formats.Add(prop.Name);
            return Formats;
        }
    }
}
