using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClub.DAL;
using MyClub.Models;

namespace MyClub.Controllers
{
    public class IndividualTrainingsController : Controller
    {
        private MyClubContext db = new MyClubContext();

        // GET: IndividualTrainings
        public ActionResult Index()
        {
            var individualTrainings = db.IndividualTrainings.Include(i => i.Instructor).Include(i => i.User);
            return View(individualTrainings.ToList());
        }

        // GET: IndividualTrainings/Details/5
        public ActionResult Details()
        {
            var list = db.IndividualTrainings.Where(model => model.User.UserName == User.Identity.Name);

            return View(list.ToList());
        }

        // GET: IndividualTrainings/Create
        public ActionResult Create()
        {
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "ID");



            return View();
        }

        // POST: IndividualTrainings/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,InstructorID,Date,Place")] IndividualTraining individualTraining)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(x => x.UserName == User.Identity.Name).First();
                individualTraining.UserID = user.ID;
                var hour = Int32.Parse(Request.Form["hour"]);
                individualTraining.Date = new DateTime(individualTraining.Date.Year, individualTraining.Date.Month, individualTraining.Date.Day, hour, 0, 0);

                var check = db.IndividualTrainings.Any(x => x.InstructorID == individualTraining.InstructorID && x.Date.Day == individualTraining.Date.Day && x.Date.Month == individualTraining.Date.Month && x.Date.Year == individualTraining.Date.Year && x.Date.Hour == individualTraining.Date.Hour);
                if (check != true)
                {
                    db.IndividualTrainings.Add(individualTraining);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.Err = "Dany trener ma już zarezerwowany trening o tej godzinie";
                    ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "ID", individualTraining.InstructorID);
                    ViewBag.UserID = new SelectList(db.Users, "ID", "UserName", individualTraining.UserID);
                    return View(individualTraining);
                }


                return RedirectToAction("Index");
            }

            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "ID", individualTraining.InstructorID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "UserName", individualTraining.UserID);
            return View(individualTraining);
        }

        // GET: IndividualTrainings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndividualTraining individualTraining = db.IndividualTrainings.Find(id);
            if (individualTraining == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "ID", individualTraining.InstructorID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "UserName", individualTraining.UserID);
            return View(individualTraining);
        }

        // POST: IndividualTrainings/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,InstructorID,Date,Place")] IndividualTraining individualTraining)
        {
            if (ModelState.IsValid)
            {
                db.Entry(individualTraining).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "ID", individualTraining.InstructorID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "UserName", individualTraining.UserID);
            return View(individualTraining);
        }

        // GET: IndividualTrainings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndividualTraining individualTraining = db.IndividualTrainings.Find(id);
            if (individualTraining == null)
            {
                return HttpNotFound();
            }
            return View(individualTraining);
        }

        // POST: IndividualTrainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IndividualTraining individualTraining = db.IndividualTrainings.Find(id);
            db.IndividualTrainings.Remove(individualTraining);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
