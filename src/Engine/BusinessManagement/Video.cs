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

        public static IList<Dbo.Video> ListUserVideos(int userId, Dbo.Video.Order order = Dbo.Video.Order.Id, bool ascOrder = true, int number = -1, int page = -1)
        {
            try
            {
                return DataAccess.Video.ListUserVideos(userId, order, ascOrder, number, page);
            }
            catch (Exception e)
            {
                throw new Exception("Can't list User Videos / Error : " + e.Message);
            }
        }

        public static void AddVideo(Dbo.Video video)
        {
            try
            {
                DataAccess.Video.AddVideo(video);
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
    }
}