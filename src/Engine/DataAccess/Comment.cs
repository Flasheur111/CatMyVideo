using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Engine.DataAccess
{
    public class Comment
    {
        public static Dbo.Comment ConvertCommentToDboComment<TSource>(TSource comment) where TSource : T_Comments
        {
            Dbo.User user = User.ConvertUserToDboUser(comment.T_Users);
            Dbo.Video video = Video.ConvertVideoToDboVideo(comment.T_Videos);
           

            return new Dbo.Comment(user, video)
            {
                Id = comment.id,
                Message = comment.message,
                User = user,
                Video = video,
                PostDate = comment.post_date
            };
        }

        public static T_Comments ConvertDboCommentToCommnet(Dbo.Comment comment)
        {
            // FIX ME
            return new T_Comments()
            {
                id = comment.Id,
                message = comment.Message,
                post_date = comment.PostDate,
                video = comment.Video.Id

            };
        }
    }
}