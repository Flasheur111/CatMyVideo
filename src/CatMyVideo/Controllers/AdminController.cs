using CatMyVideo.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace CatMyVideo.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
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

        // GET: /Admin/Index
        public ActionResult Index()
        {
            return RedirectToAction("ListUsers", "Admin");
        }
        
        // GET: /Admin/ListUsers
        public ActionResult ListUsers()
        {
            ViewData["Users"] = Engine.BusinessManagement.User.ListAllUsers();
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