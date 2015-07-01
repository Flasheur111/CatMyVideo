using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static int AddVideo(Dbo.Video video)
        {
            try
            {
                using (CatMyVideoEntities context = new CatMyVideoEntities())
                {
                    T_Videos newVideo = ConvertDboVideoToVideo(video);
                    context.T_Videos.Add(newVideo);
                    context.SaveChanges();
                    return newVideo.id;
                }
            }
            catch (Exception e)
            {
                return -1;
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
                IEnumerable<T_Videos> query = context.T_Videos.Where(x => x.uploader == userId);
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

        /// <summary>
        /// Create a specific query to fetch videos which match tags passed as parameters
        /// The final list is ordered by tags order 
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="number"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IList<Dbo.Video> ListVideosByTags(IList<Dbo.Tag> tags, int number, int page)
        {
            // If there are some tags
            if (tags == null || !tags.Any())
                return ListVideos(Dbo.Video.Order.Id, true, -1, -1);

            // Anonymous function to get best visibility
            Func<string, string> countizeTag = (tag) => "COUNT (CASE WHEN tag LIKE '" + tag + "' THEN 1 END) desc";
             Func<string, string> wherizeTag = (tag) => "tag LIKE '" + tag + "'";
           
            // WHERE and ORDER BY clauses building
            string whereString = "WHERE " + String.Join(" OR ", tags.Select(x => wherizeTag(x.Name)));
            string orderByString = "ORDER BY " + String.Join(", ", tags.Select(x => countizeTag(x.Name)));

            // pagination
            string offsetAndFetch = "";
            if (number != -1 && page != -1)
                offsetAndFetch = String.Format("OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", number * page, number);

            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                // Fetching video ids
                string tagsQuery = string.Join(" ", new [] { "SELECT video FROM T_VideosTags", whereString, "GROUP BY video", orderByString, offsetAndFetch });
                var videosId = context.Database.SqlQuery<int>(tagsQuery).ToList();

                // To preserve the order #police
                var orderDico = new Dictionary<int, int>();
                for (int i = 0; i < videosId.Count; i++)
                    orderDico.Add(videosId[i], i);
                
                return context.T_Videos.Where(x => videosId.Contains(x.id)).ToList().OrderBy(x => orderDico[x.id]).Select(x => ConvertVideoToDboVideo(x)).ToList();
            }
        }
    }
}