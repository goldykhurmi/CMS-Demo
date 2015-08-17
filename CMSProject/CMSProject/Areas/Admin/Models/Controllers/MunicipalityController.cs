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
    public class MunicipalityController : Controller
    {
        private CMSEntities db = new CMSEntities();

        //
        // GET: /Admin/Municipality/

        public ActionResult Index()
        {
            return View(db.Municipalities.ToList());
        }

        //
        // GET: /Admin/Municipality/Details/5

        public ActionResult Details(int id = 0)
        {
            Municipality municipality = db.Municipalities.Find(id);
            if (municipality == null)
            {
                return HttpNotFound();
            }
            return View(municipality);
        }

        //
        // GET: /Admin/Municipality/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Municipality/Create

        [HttpPost]
        public ActionResult Create(Municipality municipality)
        {
            if (ModelState.IsValid)
            {
                db.Municipalities.Add(municipality);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(municipality);
        }

        //
        // GET: /Admin/Municipality/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Municipality municipality = db.Municipalities.Find(id);
            if (municipality == null)
            {
                return HttpNotFound();
            }
            return View(municipality);
        }

        //
        // POST: /Admin/Municipality/Edit/5

        [HttpPost]
        public ActionResult Edit(Municipality municipality)
        {
            if (ModelState.IsValid)
            {
                db.Entry(municipality).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(municipality);
        }

        //
        // GET: /Admin/Municipality/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Municipality municipality = db.Municipalities.Find(id);
            if (municipality == null)
            {
                return HttpNotFound();
            }
            return View(municipality);
        }

        //
        // POST: /Admin/Municipality/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Municipality municipality = db.Municipalities.Find(id);
            db.Municipalities.Remove(municipality);
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