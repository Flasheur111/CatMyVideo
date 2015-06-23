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

        public enum Definition
        {
            p360,
            p480,
            p720,
            p1080,
            None
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Definition Quality { get; set; }
        public DateTime UploadDate { get; set; }
        public int BaseVideo { get; set; }
        public IList<Comment> Comments { get; private set; }
        public User Uploader { get; private set; }

        public Video(IList<Comment> comments, User uploader)
        {
            BaseVideo = BaseVideo;
            Comments = comments;
            Uploader = uploader;
        }

        public Video(){}
    }
}