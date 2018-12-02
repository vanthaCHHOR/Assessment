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
    public class USERController : Controller
    {
        private VASEntities db = new VASEntities();
        Helper AES = new Helper();

        // GET: USER
        public ActionResult Index()
        {
            if (Session["userProfile"] != null)
            {
                return View(db.USERs.ToList());
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
            return View(db.USERs.Where(r => r.USER_NAME.ToLower().Contains(searchText)).ToList());
        }

        // GET: USER/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERs.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // GET: USER/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: USER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,USER_NAME,PASSWORD,EMAIL,DESCRIPTION,CREATED_AT")] USER uSER)
        {
            if (ModelState.IsValid)
            {
                uSER.ID = Guid.NewGuid().ToString();
                uSER.PASSWORD = AES.Encrypt(uSER.PASSWORD);
                db.USERs.Add(uSER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uSER);
        }

        // GET: USER/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERs.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // POST: USER/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,USER_NAME,PASSWORD,EMAIL,DESCRIPTION,CREATED_AT")] USER uSER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uSER);
        }

        // GET: USER/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERs.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // POST: USER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            USER uSER = db.USERs.Find(id);
            db.USERs.Remove(uSER);
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
