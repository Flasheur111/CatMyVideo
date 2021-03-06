﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.BusinessManagement
{
    public class Encode
    {
        public static IList<Dbo.Encode> ListEncode()
        {
            try
            {
                return DataAccess.Encode.ListEncode();
            }
            catch (Exception e)
            {
                throw new Exception("Can't list Encode / Error : " + e.Message);
            }
        }

        public static IList<Dbo.Encode> ListEncode(int idVideo, bool isEncode)
        {
            try
            {
                return DataAccess.Encode.ListEncode(idVideo, isEncode);
            }
            catch (Exception e)
            {
                throw new Exception("Can't list Encode / Error : " + e.Message);
            }
        }

        public static List<Dbo.Encode> ListNotEncode()
        {
            try
            {
                return DataAccess.Encode.ListNotEncode();
            }
            catch (Exception e)
            {
                throw new Exception("Can't list not Encode / Error : " + e.Message);
            }
        }

        public static int AddEncode(Dbo.Encode encode)
        {
            try
            {
                return DataAccess.Encode.AddEncode(encode);
            }
            catch (Exception e)
            {
                throw new Exception("Can't add Encode / Error : " + e.Message);
            }
        }

        public static void UpdateEncode(Dbo.Encode encode)
        {
            try
            {
                DataAccess.Encode.UpdateEncode(encode);
            }
            catch (Exception e)
            {
                throw new Exception("Can't update Encode / Error : " + e.Message);
            }
        }

        public static void DeleteEncode(Dbo.Encode encode)
        {
            try
            {
                DataAccess.Encode.DeleteEncode(encode.Id);
            }
            catch (Exception e)
            {
                throw new Exception("Can't delete Encode / Error : " + e.Message);
            }
        }
    }
}
