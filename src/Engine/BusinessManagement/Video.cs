
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Engine.BusinessManagement
{
    public class Video
    {
        public static IList<Dbo.Video> ListVideos(Dbo.Video.Order order = Dbo.Video.Order.Id, bool ascOrder = true, int number = -1, int page = -1)
        {
            try
            {
                return DataAccess.Video.ListVideos(order, ascOrder, number, page);
            }
            catch (Exception e)
            {
                throw new Exception("Can't list Videos / Error : " + e.Message);
            }
        }
        public static IList<Dbo.Video> ListVideos(out int count, Dbo.Video.Order order = Dbo.Video.Order.Id, bool ascOrder = true, int number = -1, int page = -1)
        {
            try
            {
                return DataAccess.Video.ListVideos(out count, order, ascOrder, number, page);
            }
            catch (Exception e)
            {
                throw new Exception("Can't list Videos / Error : " + e.Message);
            }
        }

        public static IList<Dbo.Video> ListUserVideos(int userId, Dbo.Video.Order order = Dbo.Video.Order.Id, bool ascOrder = true, int number = -1, int page = -1, bool encoded = false)
        {
            try
            {
                return DataAccess.Video.ListUserVideos(userId, order, ascOrder, number, page, encoded);
            }
            catch (Exception e)
            {
                throw new Exception("Can't list User Videos / Error : " + e.Message);
            }
        }

        public static IList<Dbo.Video> ListUserVideos(out int count, int userId, Dbo.Video.Order order = Dbo.Video.Order.Id, bool ascOrder = true, int number = -1, int page = -1, bool encoded = false)
        {
            try
            {
                return DataAccess.Video.ListUserVideos(out count, userId, order, ascOrder, number, page, encoded);
            }
            catch (Exception e)
            {
                throw new Exception("Can't list User Videos / Error : " + e.Message);
            }
        }

        public static Dbo.Video GetVideo(int id, bool encoded = false)
        {
            try
            {
                return DataAccess.Video.GetVideo(id, encoded);
            }
            catch (Exception e)
            {
                throw new Exception("Can't find Video / Errror : " + e.Message);
            }
        }

        public static int AddVideo(Dbo.Video video)
        {
            try
            {
                return DataAccess.Video.AddVideo(video);
            }
            catch (Exception e)
            {
                throw new Exception("Can't add Video / Error : " + e.Message);
            }
       }

        public static void UpdateVideo(Dbo.Video video)
        {
            try
            {
                DataAccess.Video.UpdateVideo(video);
            }
            catch (Exception e)
            {
                throw new Exception("Can't update Video / Error : " + e.Message);
            }
        }

        public static void DeleteVideo(Dbo.Video video)
        {
            try
            {
                DataAccess.Video.DeleteVideo(video.Id);
            }
            catch (Exception e)
            {
                throw new Exception("Can't delete Video / Error : " + e.Message);
            }
        }

        public static List<Dbo.Encode> ListVideoEncode(Dbo.Video v)
        {
            try
            {
                return Encode.ListEncode().Where(x => x.Video == v.Id).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Can't list Video encode / Error : " + e.Message);
            }
        }

        public static List<Dbo.Encode> ListVideoEncode(Dbo.Encode.Encoding encoding, Dbo.Video v)
        {
            try
            {
                bool encode = encoding == Dbo.Encode.Encoding.Encoded;
                return Encode.ListEncode(v.Id, encode).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Can't list Video encode / Error : " + e.Message);
            }
        }

        public static IList<Dbo.Video> ListVideosByTags(IList<Dbo.Tag> tags, bool encoded = false, int number = -1, int page = -1)
        {
            try
            {
                return DataAccess.Video.ListVideosByTags(tags, number, page, encoded);
            }
            catch (Exception e)
            {
                throw new Exception("Can't list Videos by tags / Error : " + e.Message);
            }
        }
        public static IList<Dbo.Video> ListVideosByTags(out int count, IList<Dbo.Tag> tags, bool encoded = false, int number = -1, int page = -1)
        {
            try
            {
                return DataAccess.Video.ListVideosByTags(out count, tags, number, page, encoded);
            }
            catch (Exception e)
            {
                throw new Exception("Can't list Videos by tags / Error : " + e.Message);
            }
        }

        public static IList<Dbo.Video> ListVideosByName(string name, bool encoded = false, int number = -1, int page = -1)
        {
            try
            {
                return DataAccess.Video.ListVideosByName(name, number, page, encoded);
            }
            catch (Exception e)
            {
                throw new Exception("Can't list Videos by tags / Error : " + e.Message);
            }
        }
        public static IList<Dbo.Video> ListVideosByName(out int count, string name, bool encoded = false, int number = -1, int page = -1)
        {
            try
            {
                return DataAccess.Video.ListVideosByName(out count, name, number, page, encoded);
            }
            catch (Exception e)
            {
                throw new Exception("Can't list Videos by tags / Error : " + e.Message);
            }
        }

        public static IList<Dbo.Video> ListVideosByAuthor(string author, bool encoded = false, int number = -1, int page = -1)
        {
            try
            {
                return DataAccess.Video.ListVideosByAuthor(author, number, page, encoded);
            }
            catch (Exception e)
            {
                throw new Exception("Can't list Videos by tags / Error : " + e.Message);
            }
        }
        public static IList<Dbo.Video> ListVideosByAuthor(out int count, string author, bool encoded = false, int number = -1, int page = -1)
        {
            try
            {
                return DataAccess.Video.ListVideosByAuthor(out count, author, number, page, encoded);
            }
            catch (Exception e)
            {
                throw new Exception("Can't list Videos by tags / Error : " + e.Message);
            }
        }
    
        public static void IncrementViewCount(int videoId)
        {
            try
            {
                DataAccess.Video.AddViewCount(videoId);
            }
            catch (Exception e)
            {
                throw new Exception("Can't increment view count for " + videoId);
            }
        }
    }
}