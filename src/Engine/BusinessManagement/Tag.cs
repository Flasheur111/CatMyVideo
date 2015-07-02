using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.BusinessManagement
{
    public static class Tag
    {
        public static IList<Dbo.Tag> ListAllTags()
        {
            try
            {
                return DataAccess.Tag.ListAllTags();
            }
            catch (Exception ex)
            {
                throw new Exception("Can't list all tags. Err : " + ex.ToString());
            }
        }

        public static IList<Dbo.Tag> ListTagsByVideoId(int videoId)
        {
            try
            {
                return DataAccess.Tag.ListTagsByVideoId(videoId);
            }
            catch (Exception ex)
            {
                throw new Exception("Can't list tags for this video (" + videoId + "). Err : " + ex.ToString());
            }
        }

        public static void AddTags(IEnumerable<Dbo.Tag> tags, int videoId)
        {
            try
            {
                DataAccess.Tag.AddTags(tags, videoId);
            }
            catch (Exception ex)
            {
                throw new Exception("Can't add tags to video (" + videoId + "). Err : " + ex.ToString());
            }
        }
    }
}
