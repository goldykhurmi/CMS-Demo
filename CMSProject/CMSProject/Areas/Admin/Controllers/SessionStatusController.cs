using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMSProject.Areas.Admin.Models;

namespace CMSProject.Areas.Admin.Controllers
{
    public class SessionStatusController : Controller
    {
        private CMSEntities db = new CMSEntities();

        //
        // GET: /Admin/SessionStatus/

        public ActionResult Index()
        {
            return View(db.SessionStatuses.ToList());
        }

        //
        // GET: /Admin/SessionStatus/Details/5

        public ActionResult Details(int id = 0)
        {
            SessionStatus sessionstatus = db.SessionStatuses.Find(id);
            if (sessionstatus == null)
            {
                return HttpNotFound();
            }
            return View(sessionstatus);
        }

        //
        // GET: /Admin/SessionStatus/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/SessionStatus/Create

        [HttpPost]
        public ActionResult Create(SessionStatus sessionstatus)
        {
            if (ModelState.IsValid)
            {
                db.SessionStatuses.Add(sessionstatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sessionstatus);
        }

        //
        // GET: /Admin/SessionStatus/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SessionStatus sessionstatus = db.SessionStatuses.Find(id);
            if (sessionstatus == null)
            {
                return HttpNotFound();
            }
            return View(sessionstatus);
        }

        //
        // POST: /Admin/SessionStatus/Edit/5

        [HttpPost]
        public ActionResult Edit(SessionStatus sessionstatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sessionstatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sessionstatus);
        }

        //
        // GET: /Admin/SessionStatus/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SessionStatus sessionstatus = db.SessionStatuses.Find(id);
            if (sessionstatus == null)
            {
                return HttpNotFound();
            }
            return View(sessionstatus);
        }

        //
        // POST: /Admin/SessionStatus/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SessionStatus sessionstatus = db.SessionStatuses.Find(id);
            db.SessionStatuses.Remove(sessionstatus);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}