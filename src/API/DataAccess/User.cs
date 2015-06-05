using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.DataAccess
{
    public static class User
    {
        private static Dbo.User ConvertUserToDboUser(DataAccess.T_Users user, API.Dbo.Role type)
        {
            return new Dbo.User()
            {
                Id = user.id,
                Nickname = user.nickname,
                Mail = user.mail,
                Password = user.pass,
                Description = user.description,
                Type = type
            };
        }
        private static T_Users ConvertDboUserToUser(Dbo.User user)
        {
            T_Users newUser = null;

            switch (user.Type)
            {
                case API.Dbo.Role.Classic:
                    newUser = new T_User();
                    break;
                case API.Dbo.Role.Modo:
                    newUser = new T_Moderator();
                    break;
                case API.Dbo.Role.Admin:
                    newUser = new T_Admin();
                    break;
                default:
                    break;
            }
            newUser.mail = user.Mail;
            newUser.nickname = user.Nickname;
            newUser.pass = "*****";
            newUser.description = user.Description;
            return newUser;
        }
        public static IEnumerable<Dbo.User> ListUsers()
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                return context.T_Users.OfType<T_User>().ToList().Select(x => ConvertUserToDboUser(x, Dbo.Role.Classic));
            }
        }
        public static IEnumerable<Dbo.User> ListAdmins()
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                return context.T_Users.OfType<T_Admin>().ToList().Select(x => ConvertUserToDboUser(x, Dbo.Role.Admin));
            }
        }
        public static IEnumerable<Dbo.User> ListModerators()
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                return context.T_Users.OfType<T_Moderator>().ToList().Select(x => ConvertUserToDboUser(x, Dbo.Role.Modo));
            }
        }
        public static void AddUser(Dbo.User user)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Users newUser = ConvertDboUserToUser(user);
                context.T_Users.Add(newUser);
                context.SaveChanges();
            }
        }
        public static void UpdateUser(Dbo.User user)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Users newUser = ConvertDboUserToUser(user);
                context.T_Users.Attach(newUser);
                context.Entry(newUser).State = System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public static void DeleteUser(Guid guid)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Users newUser = new T_User() { id = guid };
                context.T_Users.Attach(newUser);
                context.T_Users.Remove(newUser);
                context.SaveChanges();
            }
        }

        public static Dbo.User FindUserById(Guid id)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Users user = context.T_Users.First(x => x.id == id);

                return ConvertUserToDboUser(user, user is T_User ? API.Dbo.Role.Classic : (user is T_Moderator ? API.Dbo.Role.Modo : API.Dbo.Role.Admin));
            }
        }
    }
}