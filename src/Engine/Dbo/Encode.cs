using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Engine.Dbo
{
    public class Encode
    {
        public enum Encoding
        {
            Encoded,
            NotYedEncoded
        }

        public enum Definition
        {
            p480,
            p720,
            p1080,
            None
        }

        public int Id { get; set; }
        public bool IsBase { get; set; }
        public bool IsEncode { get; set; }
        public string InputFormat { get; set; }
        public Definition Quality { get; set; }

        public int Video { get; set; }

        public Encode() { }

        public Encode(Definition quality,string inputFormat, int video, bool isBase)
        {
            IsBase = isBase;
            IsEncode = false;
            Quality = quality;
            Video = video;
            InputFormat = inputFormat;
        }
    }
}