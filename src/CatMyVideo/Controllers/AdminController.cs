using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Engine.BusinessManagement;

namespace CatMyVideo.Controllers
{
    public class AdminController : Controller
    {
        // GET: /Admin/Index
        public ActionResult Index()
        {
            ViewData["Users"] = Engine.BusinessManagement.User.ListAllUsers();
            return View();
        }

        // GET: /Admin/ListUsers
        public ActionResult ListUsers()
        {
            return Index();
        }

        // GET: /Admin/ListVideos
        public ActionResult ListVideos()
        {
            ViewData["Videos"] = Engine.BusinessManagement.Video.ListVideos();
            return View();
        }

        // GET: /Admin/ListComments
        public ActionResult ListComments()
        {
            ViewData["Comments"] = Engine.BusinessManagement.Comment.ListComment();
            return View();
        }
    }
}