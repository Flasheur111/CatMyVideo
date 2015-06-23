using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Engine.Dbo
{
    public class Video
    {
        public enum Order
        {
            Id
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
        public int ViewCount { get; set; }
        public int User { get; set; }
        public List<Dbo.Encode> Encodes { get; set; }

        public Video() {}
    }
}