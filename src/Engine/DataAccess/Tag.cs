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
                return context.T_Videos.First(x => x.id == videoId).T_Tags.ToList().Select(x => new Dbo.Tag() { Name = x.name }).ToList();
            }
        }

        public static void AddTag(Dbo.Tag tag, int video)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Tags tmpTag = context.T_Tags.FirstOrDefault(x => x.name == tag.Name);

                // If we can't find any tag, create a new one  
                if (tmpTag == default(T_Tags))
                {
                    tmpTag = new T_Tags();

                    tmpTag.name = tag.Name;
                    var tmpVideo = context.T_Videos.First(x => x.id == video);
                    tmpVideo.T_Tags.Add(tmpTag);
                }
                else
                {
                    tmpTag.T_Videos.Add(context.T_Videos.First(x => x.id == video));
                    context.T_Tags.Attach(tmpTag);
                    context.Entry(tmpTag).State = System.Data.Entity.EntityState.Modified;
                }

                context.SaveChanges();
            }
        }
    }
}
