using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssignmentOkoone.Models;

namespace AssignmentOkoone.Controllers
{
    public class ARTICLEController : Controller
    {
        private VASEntities db = new VASEntities();

        // GET: ARTICLE
        public ActionResult Index()
        {
            if (Session["userProfile"] != null)
            {
                return View(db.ARTICLEs.ToList());
            }
            else
            {
                return Redirect("/Account/Login");
            }
        }

        [HttpPost]
        public ActionResult Index(string searchText)
        {
            searchText = searchText.ToLower();
            return View(db.ARTICLEs.Where(r => r.ARTICLE_TITLE.ToLower().Contains(searchText)).ToList());
        }

        public ActionResult Read(string searchText)
        {
            searchText = searchText.ToLower();
            return View(db.ARTICLEs.Where(r => r.ARTICLE_TITLE.ToLower().Contains(searchText)));
        }

        // GET: ARTICLE/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARTICLE aRTICLE = db.ARTICLEs.Find(id);
            if (aRTICLE == null)
            {
                return HttpNotFound();
            }
            return View(aRTICLE);
        }

        // GET: ARTICLE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ARTICLE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ARTICLE_TITLE,DESCRIPTION,CREATED_AT")] ARTICLE aRTICLE)
        {
            if (ModelState.IsValid)
            {
                aRTICLE.ID = Guid.NewGuid().ToString();
                db.ARTICLEs.Add(aRTICLE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aRTICLE);
        }

        // GET: ARTICLE/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARTICLE aRTICLE = db.ARTICLEs.Find(id);
            if (aRTICLE == null)
            {
                return HttpNotFound();
            }
            return View(aRTICLE);
        }

        // POST: ARTICLE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ARTICLE_TITLE,DESCRIPTION,CREATED_AT")] ARTICLE aRTICLE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aRTICLE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aRTICLE);
        }

        // GET: ARTICLE/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARTICLE aRTICLE = db.ARTICLEs.Find(id);
            if (aRTICLE == null)
            {
                return HttpNotFound();
            }
            return View(aRTICLE);
        }

        // POST: ARTICLE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ARTICLE aRTICLE = db.ARTICLEs.Find(id);
            db.ARTICLEs.Remove(aRTICLE);
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
