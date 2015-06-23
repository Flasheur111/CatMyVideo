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

        public static void AddVideo(Dbo.Video video)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Videos newVideo = ConvertDboVideoToVideo(video);
                context.T_Videos.Add(newVideo);
                context.SaveChanges();
            }
        }

        public static void UpdateVideo(Dbo.Video video)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Videos newVideo = ConvertDboVideoToVideo(video);
                context.T_Videos.Attach(newVideo);
                context.Entry(newVideo).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public static void DeleteVideo(int id)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Videos newVideo = new T_Videos() { id = id };
                context.T_Videos.Attach(newVideo);
                context.T_Videos.Remove(newVideo);
                context.SaveChanges();
            }
        }

        public static IList<Dbo.Video> ListUserVideos(int userId, Dbo.Video.Order order, bool ascOrder, int number, int page)
        {
            Func<T_Videos, Object> requestOrder = null;

            switch (order)
            {
                case Engine.Dbo.Video.Order.Id:
                    requestOrder = x => x.id;
                    break;
                default:
                    requestOrder = x => x.id;
                    break;
            }

            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                IEnumerable<T_Videos> query = context.T_Videos.Where(x => x.T_Users.id == userId).ToList();
                // Order
                if (ascOrder)
                    query = query.OrderBy(requestOrder);
                else
                    query = query.OrderByDescending(requestOrder);

                // Pagination
                if (number != -1 && page != -1)
                    query = query.Skip(number * page).Take(number);

                return query.ToList().Select(x => ConvertVideoToDboVideo<T_Videos>(x)).ToList();
            }
        }

        public static IList<Dbo.Video> ListVideos(Dbo.Video.Order order, bool ascOrder, int number, int page)
        {
            Func<T_Videos, Object> requestOrder = null;

            switch (order)
            {
                case Engine.Dbo.Video.Order.Id:
                    requestOrder = x => x.id;
                    break;
                default:
                    requestOrder = x => x.id;
                    break;
            }

            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                IEnumerable<T_Videos> query = context.T_Videos.OfType<T_Videos>();
                // Order
                if (ascOrder)
                    query = query.OrderBy(requestOrder);
                else
                    query = query.OrderByDescending(requestOrder);

                // Pagination
                if (number != -1 && page != -1)
                    query = query.Skip(number * page).Take(number);

                return query.ToList().Select(x => ConvertVideoToDboVideo<T_Videos>(x)).ToList();
            }
        }

        public static IList<Dbo.Video> ListVideosToEncode()
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                IEnumerable<T_Videos> query = context.T_Videos.Where(x => x.is_encoded == false);
                return query.ToList().Select(x => ConvertVideoToDboVideo<T_Videos>(x)).ToList();
            }
        }
    }
}