using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CatMyVideo.Models;
using System.Collections.Generic;

namespace CatMyVideo.Controllers
{
  [Authorize]
  public class AccountController : Controller
  {
    private ApplicationUserManager _userManager;

    public AccountController()
    {
    }

    public AccountController(ApplicationUserManager userManager)
    {
      UserManager = userManager;
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
      }

      base.Dispose(disposing);
    }

    [Route("Account/Display/{nickname}", Name = "ShowProfile")]
    public ActionResult Display(string nickname)
    {
      if (String.IsNullOrEmpty(nickname))
        return RedirectToAction("Index", "Home");

      // Mock user 
      var user = new Engine.Dbo.User()
      {
        Id = 42,
        Mail = "coucou@gmail.com",
        Nickname = "Seika",
        Password = "coucou",
        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse iaculis egestas sodales. Nunc aliquam velit eu nulla ullamcorper, a vulputate diam vehicula. Quisque ultricies lacus ac lectus dignissim laoreet. Phasellus rutrum molestie vestibulum. In feugiat vestibulum orci a pharetra. Suspendisse et orci nisl. Phasellus at blandit nulla. Nullam laoreet odio non dui dignissim volutpat a ut nulla. Integer non enim id orci eleifend ultrices.",
        Type = Engine.Dbo.User.Role.Classic
      };

      // Mock videos
      var videos = new List<Engine.Dbo.Video>();

      for (int i = 0; i < 6; i++)
      {
        var video = new Engine.Dbo.Video()
        {
          Id = i,
          Title = "Coucou " + i,
          UploadDate = DateTime.Now,
          User = user.Id,
          ViewCount = 14 + i,
          Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse iaculis egestas sodales. Nunc aliquam velit eu nulla ullamcorper, a vulputate diam vehicula. Quisque ultricies lacus ac lectus dignissim laoreet. Phasellus rutrum molestie vestibulum. In feugiat vestibulum orci a pharetra. Suspendisse et orci nisl. Phasellus at blandit nulla. Nullam laoreet odio non dui dignissim volutpat a ut nulla. Integer non enim id orci eleifend ultrices.",
        };

        videos.Add(video);
      }

      // TODO: Replace the code above by this one, to remove mock data:
      //var user = Engine.BusinessManagement.User.FindUserByNickname(nickname);
      //var videos = Engine.BusinessManagement.Video.ListUserVideos(user.Id);
      ViewData["videos"] = videos;

      return View("Display", user);
    }

    [Route("Account/Edit/{nickname}")]
    public ActionResult Edit(string nickname)
    {
      // TODO: Check if edit is available to current user.

      // Mock user
      EditViewModel user = new EditViewModel() {
        Nickname = "Seika",
        Email = "nocteaestiva@gmail.com",
      };

      // TODO: replace the code above by this one, when removing mock.
      //var user = Engine.BusinessManagement.User.FindUserByNickname(nickname);
      return View("Edit", user);
    }

    #region Helpers
    private IAuthenticationManager AuthenticationManager
    {
      get
      {
        return HttpContext.GetOwinContext().Authentication;
      }
    }

    private void AddErrors(IdentityResult result)
    {
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError("", error);
      }
    }

    private ActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }
      return RedirectToAction("Index", "Home");
    }
    #endregion
  }
}
