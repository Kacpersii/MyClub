using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyClub.DAL;
using MyClub.Models;

namespace MyClub.Controllers
{
    public class ArticleController : Controller
    {
        private MyClubContext db = new MyClubContext();

        // GET: Article
        public ActionResult Index()
        {
            var articles = db.Articles;
            return View(articles.ToList());
        }

        // GET: Article/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.Answers = db.Answers.Where(a => a.ArticleID == article.ID).ToList();

            return View(article);
        }

        // GET: Article/Create

        [Authorize(Roles = "Admin, Trainer, User")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Article/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Trainer, User")]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                article.UserID = db.Users.Single(u => u.UserName == User.Identity.Name).ID;
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddAnswer(int articleID, string answer)
        {
            Answer newAnswer = new Answer
            {
                ArticleID = articleID,
                UserID = db.Users.Single(u => u.UserName == User.Identity.Name).ID,
                UsersAnswer = answer,
                Date = DateTime.Now
            };
            db.Answers.Add(newAnswer);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = articleID });
        }

        // GET: Article/Edit/5
        [Authorize(Roles = "Admin, Trainer")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Article/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Trainer")]
        public ActionResult Edit([Bind(Include = "ID,UserID,Subject,Content")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Article/Delete/5
        [Authorize(Roles = "Admin, Trainer")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Article/Delete/5
        [HttpPost, ActionName("DeleteArticle")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Trainer")]
        public ActionResult DeleteArticle(int id)
        {
            foreach (var item in db.Answers.Where(a => a.ArticleID == id))
            {
                db.Answers.Remove(item);
            }

            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpPost, ActionName("DaleteAnswer")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DaleteAnswer(int id)
        {
            Answer answer = db.Answers.Find(id);
            db.Answers.Remove(answer);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = answer.ArticleID });
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
