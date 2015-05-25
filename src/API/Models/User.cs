using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Dbo
{
    public class User
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string Mail { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
        public Role Type { get; set; }
    }

    public enum Role : int
    {
        Classic = 0,
        Modo = 1,
        Admin = 2
    }
}