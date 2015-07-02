using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Engine.BusinessManagement
{
    public class Comment
    {
        public static IList<Dbo.Comment> ListComment()
        {
            try
            {
                return DataAccess.Comment.ListComment();
            }
            catch (Exception e)
            {
                throw new Exception("Can't list Comment / Error : " + e.Message);
            }
        }

        public static void AddComment(Dbo.Comment comment)
        {
            try
            {
                DataAccess.Comment.AddComment(comment);
            }
            catch (Exception e)
            {
                throw new Exception("Can't add Comment / Error : " + e.Message);
            }
        }

        public static void UpdateComment(Dbo.Comment comment)
        {
            try
            {
                DataAccess.Comment.UpdateComment(comment);
            }
            catch (Exception e)
            {
                throw new Exception("Can't update Comment / Error : " + e.Message);
            }
        }

        public static void DeleteComment(Dbo.Comment comment)
        {
            try
            {
                DataAccess.Comment.DeleteComment(comment.Id);
            }
            catch (Exception e)
            {
                throw new Exception("Can't delete Comment / Error : " + e.Message);
            }
        }

        public static IList<Dbo.Comment> ListCommentByVideoId(int videoId, int number = -1, int page = -1)
        {
            try
            {
                return DataAccess.Comment.ListCommentByVideoId(videoId, number, page);
            }
            catch (Exception e)
            {
                throw new Exception("Can't list comments for this video (" + videoId + ") / Error : " + e.Message);
            }
        }
    }
}