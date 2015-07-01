using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Engine.DataAccess
{
    public class Encode
    {
        public static Dbo.Encode ConvertEncodeToDboEncode<TSource>(TSource encode) where TSource : T_Encode
        {
            return new Dbo.Encode()
            {
                Id = encode.id,
                IsBase = encode.is_base,
                IsEncode = encode.is_encoded,
                InputFormat = encode.input_format,
                Video = encode.video,
                Quality =  
                            (encode.quality == 1 ? Dbo.Encode.Definition.p480 :
                            (encode.quality == 2 ? Dbo.Encode.Definition.p720 :
                            (encode.quality == 3 ? Dbo.Encode.Definition.p1080 : Dbo.Encode.Definition.None)))
            };
        }

        public static T_Encode ConvertDboEncodeToEncode(Dbo.Encode encode)
        {
            T_Encode Encode = new T_Encode();
            Encode.id = encode.Id;
            Encode.is_base = encode.IsBase;
            Encode.is_encoded = encode.IsEncode;
            Encode.video = encode.Video;
            Encode.input_format = encode.InputFormat;
            Encode.quality =
                          (encode.Quality == Dbo.Encode.Definition.p480 ? 1 :
                          (encode.Quality == Dbo.Encode.Definition.p720 ? 2 :
                          (encode.Quality == Dbo.Encode.Definition.p1080 ? 3 : -1)));
            return Encode;
        }

        public static List<Dbo.Encode> ListEncode()
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                return context.T_Encode.Select(x => Encode.ConvertEncodeToDboEncode(x)).ToList();
            }
        }

        public static List<Dbo.Encode> ListEncode(int idVideo, bool isEncode)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                return context.T_Encode.Where(x => x.video == idVideo && x.is_encoded == isEncode).Select(x => Encode.ConvertEncodeToDboEncode(x)).ToList<Dbo.Encode>();
            }
        }

        public static int AddEncode(Dbo.Encode encode)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Encode newEncode = ConvertDboEncodeToEncode(encode);
                context.T_Encode.Add(newEncode);
                context.SaveChanges();
                return newEncode.id;
            }
        }

        public static void UpdateEncode(Dbo.Encode encode)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Encode newEncode = ConvertDboEncodeToEncode(encode);
                context.T_Encode.Attach(newEncode);
                context.Entry(newEncode).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public static void DeleteEncode(int id)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Encode newEncode = new T_Encode() { id = id };
                context.T_Encode.Attach(newEncode);
                context.T_Encode.Remove(newEncode);
                context.SaveChanges();
            }
        }
    }
}