using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatMyVideo.BusinessManagement
{
    public class Video
    {
        public static IList<Dbo.Video> ListVideos(Dbo.Video.Order order = Dbo.Video.Order.Id, bool ascOrder = true, int number = -1, int page = -1)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IList<Dbo.Video> ListUserVideos(int userId, Dbo.Video.Order order = Dbo.Video.Order.Id, bool ascOrder = true, int number = -1, int page = -1)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void AddVideo()
        {
            // Todo
        }

        public static void UpdateVideo()
        {
            // Todo
        }

        public static void DeleteVideo()
        {
            // Todo
        }
    }
}