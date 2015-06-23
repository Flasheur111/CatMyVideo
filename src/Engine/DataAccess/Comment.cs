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
            return new Dbo.Comment()
            {
                Id = comment.id,
                Message = comment.message,
                User = comment.author,
                Video = comment.video,
                PostDate = comment.post_date
            };
        }

        public static T_Comments ConvertDboCommentToComment(Dbo.Comment comment)
        {
            return new T_Comments()
            {
                id = comment.Id,
                message = comment.Message,
                post_date = comment.PostDate,
                video = comment.Video,
                author = comment.User
            };
        }

        public static List<Dbo.Comment> ListComment()
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                return context.T_Comments.Select(x => Comment.ConvertCommentToDboComment(x)).ToList();
            }
        }

        public static void AddComment(Dbo.Comment comment)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Comments newComment = Comment.ConvertDboCommentToComment(comment);
                context.T_Comments.Add(newComment);
                context.SaveChanges();
            }
        }

        public static void UpdateComment(Dbo.Comment comment)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Comments newComment = Comment.ConvertDboCommentToComment(comment);
                context.T_Comments.Attach(newComment);
                context.Entry(newComment).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public static void DeleteComment(int id)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Comments newComment = new T_Comments() { id = id };
                context.T_Comments.Attach(newComment);
                context.T_Comments.Remove(newComment);
                context.SaveChanges();
            }
        }
    }
}