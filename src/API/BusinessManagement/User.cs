using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.BusinessManagement
{
    public static class User
    {
        public static IEnumerable<Dbo.User> ListClassicUsers()
        {
            return DataAccess.User.ListUsers();
        }

        public static IEnumerable<Dbo.User> ListAdminUsers()
        {
            return DataAccess.User.ListAdmins();
        }

        public static IEnumerable<Dbo.User> ListModoUsers()
        {
            return DataAccess.User.ListModerators();
        }

        public static void AddUser(Dbo.User user)
        {
            DataAccess.User.AddUser(user);
        }

        public static void UpdateUser(Dbo.User user)
        {
            DataAccess.User.UpdateUser(user);
        }

        public static void DeleteUser(string id)
        {
            DataAccess.User.DeleteUser(Guid.Parse(id));
        }

        public static Dbo.User FindUser(string id)
        {
            return DataAccess.User.FindUserById(Guid.Parse(id));
        }
    }
}