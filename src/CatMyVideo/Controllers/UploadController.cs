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
            Converter.Models.Format fmanager = new Converter.Models.Format();
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

            string ListFormats = "";
            fmanager.Formats.ForEach(x => ListFormats += "." + x + ",");
            ViewData["Format"] = ListFormats.Substring(0, ListFormats.Length - 2);

            return View();
        }
    }
}