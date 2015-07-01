using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.BusinessManagement
{
    public class Role
    {
        public static IList<Dbo.Role> ListAllRoles()
        {
            try
            {
                return DataAccess.Role.ListAllRoles();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
