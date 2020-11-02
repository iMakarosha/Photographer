using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Photographer.Models;
using Photographer.Services;

namespace Photographer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View(new AboutViewModel
            {
                photos = new PhotoService().GetAlbumPhotos(7).ToList()
            });
        }

        public ActionResult Portfolio()
        {
            return View(new PhotoService().GetPortfolio());
        }

        public ActionResult Service()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult NotFound(string message)
        {
            ViewBag.Message = message;
            return PartialView();
        }

        public JsonResult AddBooking(string name, string email, string phone, string comment, string service)
        {
            try
            {
                new PhotoService().AddBooking(name, email, phone, comment, Convert.ToInt32(service));
                return Json("ok");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}