using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Engine.DataAccess
{
    public class Video
    {
        public static Dbo.Video ConvertVideoToDboVideo<TSource>(TSource video) where TSource : T_Videos
        {
            List<Dbo.Comment> comments = new List<Dbo.Comment>();
            Dbo.User uploader = User.ConvertUserToDboUser(video.T_Users);

            foreach (T_Comments t_comment in video.T_Comments)
            {
                Dbo.Comment comment = Comment.ConvertCommentToDboComment(t_comment);
                comments.Add(comment);
            }

            Dbo.Video.Definition definition = Dbo.Video.Definition.None;
            switch (video.baseVideo)
            {
                case 0: 
                    definition = Dbo.Video.Definition.p360;
                    break;
                case 1:
                    definition = Dbo.Video.Definition.p480;
                    break;
                case 2:
                    definition = Dbo.Video.Definition.p720;
                    break;
                case 3:
                    definition = Dbo.Video.Definition.p1080;
                    break;
            }

            return new Dbo.Video(comments, uploader)
            {
                Id = video.id,
                Description = video.description,
                Quality = definition,
                UploadDate = video.upload_date,
                Title = video.title,
                BaseVideo = video.baseVideo
            };
        }
        public static T_Videos ConvertDboVideoToVideo(Dbo.Video video)
        {

            // FIX ME
            T_Videos Video = new T_Videos();
            Video.id = video.Id;
            return Video;
        }
    }
}