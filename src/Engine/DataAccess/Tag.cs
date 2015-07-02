using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.DataAccess
{
    public static class Tag
    {
        public static IList<Dbo.Tag> ListAllTags()
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                return context.T_Tags.ToList().Select(x => new Dbo.Tag() { Name = x.name }).ToList();
            }
        }
        public static IList<Dbo.Tag> ListTagsByVideoId(int videoId)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                return context.T_Tags.Where(x => x.T_Videos.Any(y => y.id == videoId)).ToList().Select(x => new Dbo.Tag() { Name = x.name }).ToList();
            }
        }
    }
}
