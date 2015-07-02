using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CatMyVideo.Models;

namespace CatMyVideo.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        // POST: Comment
        [Route("/Comment/Create/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, CommentViewModel model)
        {

            return RedirectToAction("Display", "Video", id);
        }
    }
}