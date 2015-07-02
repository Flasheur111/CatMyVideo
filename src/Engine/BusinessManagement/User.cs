using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Engine.BusinessManagement
{
    public static class User
    {
        public static IList<Dbo.User> ListAllUsers(Dbo.User.Order order = Dbo.User.Order.Id, bool ascOrder = true, int number = -1, int page = -1)
        {
            try
            {
                return DataAccess.User.ListAllUsers(order, ascOrder, number, page);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void AddUser(Dbo.User user)
        {
            try
            {
                DataAccess.User.AddUser(user);
            }
            catch (Exception e)
            {
                throw new Exception("Can't add user (" + user.ToString() + ")" + e.ToString());
            }
        }

        public static void UpdateUser(Dbo.User user)
        {
            try
            {
                DataAccess.User.UpdateUser(user);
            }
            catch (Exception e)
            {
                throw new Exception("Can't update user (" + user.ToString() + ")");
            }
        }

        public static void DeleteUser(int id)
        {
            try
            {
                DataAccess.User.DeleteUser(id);
            }
            catch (Exception e)
            {
                throw new Exception("Can't delete user (" + id + ") " + e.ToString());
            }
        }

        public static Dbo.User FindUser(int id)
        {
            try
            {
                return DataAccess.User.FindUserById(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Dbo.User FindUser(string email)
        {
            try
            {
                return DataAccess.User.FindUserByEmail(email);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Dbo.User FindUserByNickname(string nickname)
        {
            try
            {
                return DataAccess.User.FindUserByNickname(nickname);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}