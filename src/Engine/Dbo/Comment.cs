﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Engine.Dbo
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime PostDate { get; set; }
        public Dbo.User User { get; set; }
        public int Video { get; set; }
    }
}