using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.DataAccess
{
    class Role
    {
        public static Dbo.Role ConvertAspNetRolesToDboRole(AspNetRoles role)
        {
            return new Dbo.Role
            {
                Value = role.Name,
            };
        }

        public static IList<Dbo.Role> ListAllRoles()
        {
            using (CatMyVideoEntities context = new CatMyVideoEntities())
            {
                IEnumerable<AspNetRoles> query = context.AspNetRoles;


                return query.ToList().Select(x => ConvertAspNetRolesToDboRole(x)).ToList();
            }
        }
    }
}
