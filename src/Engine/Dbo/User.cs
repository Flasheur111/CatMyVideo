using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Engine.Dbo
{
    public class User
    {
        public enum Order
        {
            Id,
            Nickname,
            Mail
        }

        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Mail { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
        public string AspNetUsersId { get; set; }
    }

   
}