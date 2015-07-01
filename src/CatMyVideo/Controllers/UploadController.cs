using Engine.Dbo;
using Storage.Video;
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
            {
                string fileExtension = Path.GetExtension(file.FileName).Substring(1);
                try
                {
                    RemoteFileInfo fileInfo = new RemoteFileInfo();
                    fileInfo.Stream = file.InputStream;
                    fileInfo.InputFormat = fileExtension;

                    // Add a Video { Title, Description, UserId }
                    Video video = new Video("Titre", "Description", 1);

                    fileInfo.IdVideo = Engine.BusinessManagement.Video.AddVideo(video);

                    ClientManager.UploadVideo(fileInfo);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            string ListFormats = "";
            FormatChecker.GetFormats().ForEach(x => ListFormats += "." + x + ",");
            ViewData["Format"] = ListFormats.Substring(0, ListFormats.Length - 2);

            return View();
        }
    }
}