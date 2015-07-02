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

        public static void AddTag(Dbo.Tag tag, int videoId)
        {
            try
            {
                DataAccess.Tag.AddTag(tag, videoId);
            }
            catch (Exception ex)
            {
                throw new Exception("Can't add tag (" + tag.ToString() + "). Err : " + ex.ToString());
            }
        }
    }
}
