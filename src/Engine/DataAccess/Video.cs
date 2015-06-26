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
            List<Dbo.Encode> dboEncoded = new List<Dbo.Encode>();
            foreach (T_Encode t_encode in video.T_Encode)
            {
                dboEncoded.Add(Encode.ConvertEncodeToDboEncode(t_encode));
            }
            

            return new Dbo.Video()
            {
                Id = video.id,
                Description = video.description,
                UploadDate = video.upload_date,
                Title = video.title,
                ViewCount = (int) video.view_count,
                User = video.T_Users.id,
                Encodes = dboEncoded
            };
        }
        public static T_Videos ConvertDboVideoToVideo(Dbo.Video video)
        {
            T_Videos Video = new T_Videos();
            Video.id = video.Id;
            Video.description = video.Description;
            Video.upload_date = video.UploadDate;
            Video.title = video.Title;
            Video.view_count = video.ViewCount;
            Video.uploader = video.User;
            
            return Video;
        }

        public static Dbo.Video GetVideo(int id)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Videos video = context.T_Videos.FirstOrDefault(vid => vid.id == id);
                if (video != null)
                    return ConvertVideoToDboVideo(video);
                return null;
            }
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
                IEnumerable<T_Videos> query = context.T_Videos.Where(x => x.uploader == userId).ToList();
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
    }
}