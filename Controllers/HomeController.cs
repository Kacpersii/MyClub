using MyClub.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyClub.Controllers
{
    public class HomeController : Controller
    {
        private MyClubContext db = new MyClubContext();
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                if (db.IndividualTrainings.Any(x => x.User.UserName == User.Identity.Name && x.Date.Year == DateTime.Now.Year && x.Date.Month == DateTime.Now.Month && x.Date.Day == (DateTime.Now.Day + 1)) == true)
                {
                    var it = db.IndividualTrainings.Where(x => x.User.UserName == User.Identity.Name && x.Date.Year == DateTime.Now.Year && x.Date.Month == DateTime.Now.Month && x.Date.Day == (DateTime.Now.Day + 1)).First();
                    Session["TrainingDate"] = it.Date;
                    Session["Place"] = it.Place;
                }

            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}