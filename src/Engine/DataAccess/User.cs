using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace Engine.DataAccess
{
    public static class User
    {
        public static Dbo.User ConvertUserToDboUser(AspNetUsers firstUser, T_Users secondUser)
        {
            return new Dbo.User()
            {
                Id = secondUser.id,
                Mail = firstUser.Email,
                Password = firstUser.PasswordHash,
                AspNetUsersId = firstUser.Id,
                Nickname = secondUser.nickname,
                Description = secondUser.description,
            };
        }
        public static T_Users ConvertDboUserToUser(Dbo.User user)
        {
            T_Users newUser = new T_Users()
            {
                id = user.Id,
                AspNetUsersId = user.AspNetUsersId,
                nickname = user.Nickname,
                description = user.Description
            };
            return newUser;
        }
        public static AspNetUsers ConvertDboUserToASPUser(Dbo.User user)
        {
            AspNetUsers newUser = new AspNetUsers()
            {
                Email = user.Mail,
                T_UserId = user.Id,
                PasswordHash = user.Password,
            };
            return newUser;
        }

        private static IList<Dbo.User> ListUsers<TSource>(Dbo.User.Order order, bool ascOrder, int number, int page) where TSource : T_Users
        {
            Func<TSource, Object> requestOrder = null;

            switch (order)
            {
                case Engine.Dbo.User.Order.Nickname:
                    requestOrder = x => x.nickname;
                    break;
                case Dbo.User.Order.Mail:
                    requestOrder = x => x.AspNetUsers.Email;
                    break;
                case Engine.Dbo.User.Order.Id:
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

                return query.ToList().Select(x => ConvertUserToDboUser(x.AspNetUsers, x)).ToList();
            }
        }

        public static IList<Dbo.User> ListClassics(Dbo.User.Order order, bool ascOrder, int number, int page)
        {
            return ListUsers<T_Users>(order, ascOrder, number, page);
        }
        public static IList<Dbo.User> ListModerators(Dbo.User.Order order, bool ascOrder, int number, int page)
        {
            return ListUsers<T_Users>(order, ascOrder, number, page);
        }
        public static IList<Dbo.User> ListAdmins(Dbo.User.Order order, bool ascOrder, int number, int page)
        {
            return ListUsers<T_Users>(order, ascOrder, number, page);
        }

        public static void AddUser(Dbo.User user)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                T_Users newUser = ConvertDboUserToUser(user);
                context.T_Users.Add(newUser);
                context.SaveChanges();

                AspNetUsers newAspUser = context.AspNetUsers.First(x => x.Id == newUser.AspNetUsersId);
                newAspUser.T_UserId = newUser.id;
                context.AspNetUsers.Attach(newAspUser);
                context.Entry(newAspUser).State = System.Data.Entity.EntityState.Modified;
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
                T_Users newUser = new T_Users() { id = id };
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
                AspNetUsers AspUser = user.AspNetUsers;

                MethodInfo getMethod = typeof(User).GetMethod("ConvertUserToDboUser", BindingFlags.Static | BindingFlags.NonPublic);
                MethodInfo genericGet = getMethod.MakeGenericMethod(new Type[2] { AspUser.GetType(), user.GetType() });

                return (Dbo.User)genericGet.Invoke(null, new object[] { AspUser, user });
            }
        }
        public static Dbo.User FindUserByAspNetId(string id)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                AspNetUsers AspUser = context.AspNetUsers.First(x => x.Id == id);
                T_Users user = AspUser.T_Users;

                return ConvertUserToDboUser(AspUser, user);
            }
        }
        public static Dbo.User FindUserByEmail(string email)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                AspNetUsers AspUser = context.AspNetUsers.First(x => x.Email == email);
                T_Users user = AspUser.T_Users;

                return ConvertUserToDboUser(AspUser, user);
            }
        }
       public static Dbo.User FindUserByNickname(string nickname)
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                AspNetUsers AspUser = context.AspNetUsers.First(x => x.UserName == nickname);
                T_Users user = AspUser.T_Users;

                return ConvertUserToDboUser(AspUser, user);
                /*MethodInfo getMethod = typeof(User).GetMethod("ConvertUserToDboUser", BindingFlags.Public);
                MethodInfo genericGet = getMethod.MakeGenericMethod(new Type[2] {AspUser.GetType(), user.GetType() });

                return (Dbo.User)genericGet.Invoke(null, new object[] { AspUser, user });*/
            }
        }
    }
}