using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Web;

namespace Engine.DataAccess
{
    public class Video
    {
        public static Dbo.Video ConvertVideoToDboVideo<TSource>(TSource video, bool encoded = false) where TSource : T_Videos
        {
            List<Dbo.Encode> dboEncoded = new List<Dbo.Encode>();
            foreach (T_Encode t_encode in video.T_Encode)
            {
                if (!encoded || t_encode.is_encoded)
                    dboEncoded.Add(Encode.ConvertEncodeToDboEncode(t_encode));
            }

            return new Dbo.Video()
            {
                Id = video.id,
                Description = video.description,
                UploadDate = video.upload_date,
                Title = video.title,
                ViewCountToday = ViewCountToday(video.id),
                ViewCountTotal = ViewCountTotal(video.id),
                User = DataAccess.User.FindUserById(video.T_Users.id),
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
            Video.uploader = video.User.Id;
            return Video;
        }

        public static Dbo.Video GetVideo(int id, bool encoded = false)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Videos video = context.T_Videos.FirstOrDefault(vid => vid.id == id);
                if (video != null)
                    return ConvertVideoToDboVideo(video, encoded);
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
                    newVideo.T_Users = context.T_Users.First(x => x.id == video.User.Id);
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

        public static IList<Dbo.Video> ListUserVideos(int userId, Dbo.Video.Order order, bool ascOrder, int number, int page, bool encoded)
        {
            Func<T_Videos, Object> requestOrder = null;

            switch (order)
            {
                case Engine.Dbo.Video.Order.Id:
                    requestOrder = x => x.id;
                    break;
                case Dbo.Video.Order.UploadDate:
                    requestOrder = x => x.upload_date;
                    break;
                default:
                    requestOrder = x => x.id;
                    break;
            }

            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                IEnumerable<T_Videos> query = context.T_Videos.Where(x => x.uploader == userId && (!encoded || x.T_Encode.Any(y => y.is_encoded)));
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
        public static IList<Dbo.Video> ListUserVideos(out int count, int userId, Dbo.Video.Order order, bool ascOrder, int number, int page, bool encoded)
        {
            Func<T_Videos, Object> requestOrder = null;

            switch (order)
            {
                case Engine.Dbo.Video.Order.Id:
                    requestOrder = x => x.id;
                    break;
                case Dbo.Video.Order.UploadDate:
                    requestOrder = x => x.upload_date;
                    break;
                default:
                    requestOrder = x => x.id;
                    break;
            }

            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                IEnumerable<T_Videos> query = context.T_Videos.Where(x => x.uploader == userId && (!encoded || x.T_Encode.Any(y => y.is_encoded)));
                // Order
                if (ascOrder)
                    query = query.OrderBy(requestOrder);
                else
                    query = query.OrderByDescending(requestOrder);

                count = query.Count();

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
                case Dbo.Video.Order.UploadDate:
                    requestOrder = x => x.upload_date;
                    break;
                case Dbo.Video.Order.ViewCountTotal:
                case Dbo.Video.Order.ViewCountToday:
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
        public static IList<Dbo.Video> ListVideos(out int count, Dbo.Video.Order order, bool ascOrder, int number, int page)
        {
            Func<T_Videos, Object> requestOrder = null;

            switch (order)
            {
                case Engine.Dbo.Video.Order.Id:
                    requestOrder = x => x.id;
                    break;
                case Dbo.Video.Order.UploadDate:
                    requestOrder = x => x.upload_date;
                    break;
                case Dbo.Video.Order.ViewCountTotal:
                case Dbo.Video.Order.ViewCountToday:
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

                count = query.Count();
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
        public static IList<Dbo.Video> ListVideosByTags(IList<Dbo.Tag> tags, int number, int page, bool encoded = false)
        {
            // If there are some tags
            if (tags == null || !tags.Any())
                return ListVideos(Dbo.Video.Order.Id, true, -1, -1);

            // Anonymous function to get best visibility
            Func<string, string> countizeTag = (tag) => "COUNT (CASE WHEN tag = '" + tag + "' THEN 1 END) desc";
            Func<string, string> wherizeTag = (tag) => "tag = '" + tag + "'";
            
            string whereString = "WHERE ";

            // When decoded videos are the only ones requested
            string joinString = "";
            if (encoded)
            {
                joinString = "JOIN T_Encode ON T_Encode.video = T_VideosTags.video";
                whereString += "T_Encode.is_encoded = 1 AND ";
            }

            // WHERE and ORDER BY clauses building
            whereString += String.Join(" OR ", tags.Select(x => wherizeTag(x.Name)));
            string orderByString = "ORDER BY " + String.Join(", ", tags.Select(x => countizeTag(x.Name)));

            // pagination
            string offsetAndFetch = "";
            if (number != -1 && page != -1)
                offsetAndFetch = String.Format("OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", number * page, number);

            
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                // Fetching video ids
                string tagsQuery = string.Join(" ", new[] { "SELECT T_VideosTags.video FROM T_VideosTags", joinString, whereString, "GROUP BY T_VideosTags.video", orderByString, offsetAndFetch });
                var videosId = context.Database.SqlQuery<int>(tagsQuery).ToList();

                // To preserve the order #police
                var orderDico = new Dictionary<int, int>();
                for (int i = 0; i < videosId.Count; i++)
                    orderDico.Add(videosId[i], i);

                return context.T_Videos.Where(x => videosId.Contains(x.id)).ToList().OrderBy(x => orderDico[x.id]).Select(x => ConvertVideoToDboVideo(x)).ToList();
            }
        }

        public static IList<Dbo.Video> ListVideosByTags(out int count, IList<Dbo.Tag> tags, int number, int page, bool encoded = false)
        {
            // If there are some tags
            if (tags == null || !tags.Any())
            {
                return ListVideos(out count, Dbo.Video.Order.Id, true, number, page);
            }

            // Anonymous function to get best visibility
            Func<string, string> countizeTag = (tag) => "COUNT (CASE WHEN tag = '" + tag + "' THEN 1 END) desc";
            Func<string, string> wherizeTag = (tag) => "tag = '" + tag + "'";

            string whereString = "WHERE ";

            // When decoded videos are the only ones requested
            string joinString = "";
            if (encoded)
            {
                joinString = "JOIN T_Encode ON T_Encode.video = T_VideosTags.video";
                whereString += "T_Encode.is_encoded = 1 AND ";
            }

            // WHERE and ORDER BY clauses building
            whereString += String.Join(" OR ", tags.Select(x => wherizeTag(x.Name)));
            string orderByString = "ORDER BY " + String.Join(", ", tags.Select(x => countizeTag(x.Name)));

            // pagination
            string offsetAndFetch = "";
            if (number != -1 && page != -1)
                offsetAndFetch = String.Format("OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", number * page, number);


            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                // Fetching video ids
                string tagsQuery = string.Join(" ", new[] { "SELECT T_VideosTags.video FROM T_VideosTags", joinString, whereString, "GROUP BY T_VideosTags.video", orderByString, offsetAndFetch });
                string countQuery = string.Join(" ", new[] { "SELECT COUNT(*) FROM (SELECT DISTINCT T_VideosTags.video FROM T_VideosTags", joinString, whereString, ") as request" });
                var videosId = context.Database.SqlQuery<int>(tagsQuery).ToList();
                count = context.Database.SqlQuery<int>(countQuery).First();

                // To preserve the order #police
                var orderDico = new Dictionary<int, int>();
                for (int i = 0; i < videosId.Count; i++)
                    orderDico.Add(videosId[i], i);

                return context.T_Videos.Where(x => videosId.Contains(x.id)).ToList().OrderBy(x => orderDico[x.id]).Select(x => ConvertVideoToDboVideo(x)).ToList();
            }
        }

        public static IList<Dbo.Video> ListVideosByAuthor(string author, int number, int page, bool encoded = false)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                author = author.ToLower();
                var query = (IQueryable<T_Videos>)context.T_Videos
                    .Where(x => SqlFunctions.SoundCode(x.T_Users.nickname.ToLower()) == SqlFunctions.SoundCode(author)
                                && (!encoded || x.T_Encode.Any(y => y.is_encoded)))
                    .OrderByDescending(x => x.upload_date);

                // Pagination
                if (number != -1 && page != -1)
                    query = query.Skip(number * page).Take(number);

                return query.ToList()
                .Select(x => ConvertVideoToDboVideo(x))
                .ToList();
            }
        }
        public static IList<Dbo.Video> ListVideosByAuthor(out int count, string author, int number, int page, bool encoded = false)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                author = author.ToLower();
                var query = (IQueryable<T_Videos>)context.T_Videos
                    .Where(x => SqlFunctions.SoundCode(x.T_Users.nickname.ToLower()) == SqlFunctions.SoundCode(author)
                                && (!encoded || x.T_Encode.Any(y => y.is_encoded)))
                    .OrderByDescending(x => x.upload_date);

                count = query.Count();
                // Pagination
                if (number != -1 && page != -1)
                    query = query.Skip(number * page).Take(number);

                return query.ToList()
                .Select(x => ConvertVideoToDboVideo(x))
                .ToList();
            }
        }

        public static IList<Dbo.Video> ListVideosByName(out int count, string title, int number, int page, bool encoded = false)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                title = title.ToLower();
                var query = (IQueryable<T_Videos>)context.T_Videos
                    .Where(x => SqlFunctions.SoundCode(x.title.ToLower()) == SqlFunctions.SoundCode(title)
                                && (!encoded || x.T_Encode.Any(y => y.is_encoded)))
                    .OrderByDescending(x => x.upload_date);
                
                count = query.Count();
                // Pagination
                if (number != -1 && page != -1)
                    query = query.Skip(number * page).Take(number);

                return query.ToList()
                .Select(x => ConvertVideoToDboVideo(x))
                .ToList();
            }
        }
        public static IList<Dbo.Video> ListVideosByName(string title, int number, int page, bool encoded = false)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                title = title.ToLower();
                var query = (IQueryable<T_Videos>)context.T_Videos
                    .Where(x => SqlFunctions.SoundCode(x.title.ToLower()) == SqlFunctions.SoundCode(title)
                                && (!encoded || x.T_Encode.Any(y => y.is_encoded)))
                    .OrderByDescending(x => x.upload_date);

                // Pagination
                if (number != -1 && page != -1)
                    query = query.Skip(number * page).Take(number);

                return query.ToList()
                .Select(x => ConvertVideoToDboVideo(x))
                .ToList();
            }
        }

        private static long ViewCountTotal(int videoId)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                return (from count in context.T_ViewCount
                        where count.video == videoId
                        select count.count).Sum();
            }
        }

        private static long ViewCountToday(int videoId)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                return (from count in context.T_ViewCount
                        where count.date == DateTime.Today && count.video == videoId
                        select count.count).Sum();
            }
        }

        public static void AddViewCount(int videoId)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                var count = context.T_ViewCount.FirstOrDefault(x => x.video == videoId && x.date == DateTime.Today);

                if (count == default(T_ViewCount))
                {
                    count = new T_ViewCount();
                    count.date = DateTime.Today;
                    count.count = 1;
                    count.video = videoId;
                    context.T_ViewCount.Add(count);
                }
                else
                {
                    count.count++;
                    context.T_ViewCount.Attach(count);
                    context.Entry(count).State = System.Data.Entity.EntityState.Modified;
                }
                context.SaveChanges();
            }
        }
    }
}