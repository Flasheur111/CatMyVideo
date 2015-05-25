using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class UserController : ApiController
    {
        // GET api/user
        public IEnumerable<Dbo.User> Get()
        {
           return BusinessManagement.User.ListClassicUsers();
        }

        // GET api/user/5
        public Dbo.User Get(string id)
        {
            return BusinessManagement.User.FindUser(id);
        }

        // POST api/user
        public void Post(Dbo.User user)
        {
            BusinessManagement.User.AddUser(user);
        }

        // PUT api/user/5
        public void Put(string id, Dbo.User user)
        {
            user.Id = Guid.Parse(id);
            BusinessManagement.User.UpdateUser(user);
        }

        // DELETE api/user/5
        public void Delete(string id)
        {
            BusinessManagement.User.DeleteUser(id);
        }

    }
}
