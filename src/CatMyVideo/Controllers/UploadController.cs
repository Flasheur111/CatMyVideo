using Storage.WCF;
using Storage.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatMyVideo.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    RemoteFileInfo fileInfo = new RemoteFileInfo();
                    // Read stream into byte[] buffer
                    fileInfo.Stream = file.InputStream;
                    fileInfo.FileName = file.FileName;
                    fileInfo.ContentLength = file.ContentLength;

                    ClientManager.UploadVideo(fileInfo);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View();
        }
    }
}