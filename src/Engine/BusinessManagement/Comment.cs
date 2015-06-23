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
    }
}