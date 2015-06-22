using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace CatMyVideo.DataAccess
{
    public static class User
    {
        private static Dbo.User ConvertUserToDboUser<TSource>(TSource user) where TSource : T_Users
        {
            Type userType = typeof(TSource);
            Dbo.Role userRole;

            if (userType == typeof(T_User))
                userRole = Dbo.Role.Classic;
            else if (userType == typeof(T_Moderator))
                userRole = Dbo.Role.Modo;
            else if (userType == typeof(T_Administrator))
                userRole = Dbo.Role.Admin;
            else
                throw new Exception("Oh oh");

            return new Dbo.User()
            {
                Id = user.id,
                Nickname = user.nickname,
                Mail = user.mail,
                Password = user.pass,
                Description = user.description,
                Type = userRole
            };
        }

      
        private static T_Users ConvertDboUserToUser(Dbo.User user)
        {
            T_Users newUser = null;

            switch (user.Type)
            {
                case Dbo.Role.Classic:
                    newUser = new T_User();
                    break;
                case Dbo.Role.Modo:
                    newUser = new T_Moderator();
                    break;
                case Dbo.Role.Admin:
                    newUser = new T_Administrator();
                    break;
                default:
                    break;
            }
            newUser.mail = user.Mail;
            newUser.nickname = user.Nickname;
            newUser.pass = user.Password;
            newUser.description = user.Description;
            return newUser;
        }

        private static IList<Dbo.User> ListAllUsers<TSource>(Dbo.Order order = Dbo.Order.Id, bool ascOrder = true, int number = -1, int page = -1) where TSource : T_Users
        {
            Func<TSource, Object> requestOrder = null;

            switch (order)
            {
                case CatMyVideo.Dbo.Order.Nickname:
                    requestOrder = x => x.nickname;
                    break;
                case CatMyVideo.Dbo.Order.Mail:
                    requestOrder = x => x.mail;
                    break;
                case CatMyVideo.Dbo.Order.Id:
                default:
                    requestOrder = x => x.id;
                    break;
            }

            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                IEnumerable<TSource> query = context.T_Users.OfType<TSource>();

                // Order
                if (ascOrder)
                    query = query.OrderBy(requestOrder);
                else
                    query = query.OrderByDescending(requestOrder);

                // Pagination
                if (number != -1 && page != -1)
                    query = query.Skip(number * page).Take(number);

                return query.ToList().Select(x => ConvertUserToDboUser<TSource>(x)).ToList();
            }
        }

        public static IList<Dbo.User> ListClassics()
        {
            return ListAllUsers<T_User>();
        }
        public static IList<Dbo.User> ListModerators()
        {
            return ListAllUsers<T_Moderator>();
        }
        public static IList<Dbo.User> ListAdmins()
        {
            return ListAllUsers<T_Administrator>();
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
                context.Entry(newUser).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public static void DeleteUser(int id)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Users newUser = new T_User() { id = id };
                context.T_Users.Attach(newUser);
                context.T_Users.Remove(newUser);
                context.SaveChanges();
            }
        }

        public static Dbo.User FindUserById(int id)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Users user = context.T_Users.First(x => x.id == id);
                Type userType = user is T_User ? typeof(T_User) : (user is T_Moderator ? typeof(T_Moderator) : typeof(T_Administrator));

                MethodInfo getMethod = typeof(User).GetMethod("ConvertUserToDboUser", BindingFlags.Static | BindingFlags.NonPublic);
                MethodInfo genericGet = getMethod.MakeGenericMethod(userType);

                return (Dbo.User)genericGet.Invoke(null, new object[] { user });
            }
        }

        public static Dbo.User FindUserByEmail(string email)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Users user = context.T_Users.First(x => x.mail == email);
                Type userType = user is T_User ? typeof(T_User) : (user is T_Moderator ? typeof(T_Moderator) : typeof(T_Administrator));

                MethodInfo getMethod = typeof(User).GetMethod("ConvertUserToDboUser", BindingFlags.Public);
                MethodInfo genericGet = getMethod.MakeGenericMethod(userType);

                return (Dbo.User)genericGet.Invoke(null, new object[] { user });
            }
        }
    }
}