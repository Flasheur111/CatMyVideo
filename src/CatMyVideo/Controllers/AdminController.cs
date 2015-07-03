using CatMyVideo.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace CatMyVideo.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    public class AdminController : Controller
    {
        #region init
        private ApplicationRoleManager _roleManager;
        private ApplicationUserManager _userManager;

        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }


        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>(); }
            private set { _roleManager = value; }
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_roleManager != null)
                {
                    _roleManager.Dispose();
                    _roleManager = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        // GET: /Admin/Index
        public ActionResult Index()
        {
            if (User.IsInRole("Moderator"))
                return RedirectToAction("ListVideos", "Admin");
            else
                return RedirectToAction("ListUsers", "Admin");
        }
        
        // GET: /Admin/ListUsers
        public ActionResult ListUsers()
        {
            List<Engine.Dbo.User> users = (List<Engine.Dbo.User>)Engine.BusinessManagement.User.ListAllUsers();
            if (!User.IsInRole("Admin"))
                users.RemoveAll(user => user.Roles.Count != 0);
            ViewData["Users"] = users;
            ViewData["Roles"] = Engine.BusinessManagement.Role.ListAllRoles();
            return View();
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