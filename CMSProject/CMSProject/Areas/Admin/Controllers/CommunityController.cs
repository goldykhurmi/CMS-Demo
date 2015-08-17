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
    public class CommunityController : Controller
    {
        private CMSEntities db = new CMSEntities();

        //
        // GET: /Admin/Community/

        public ActionResult Index()
        {
            var communities = db.Communities.Include(c => c.Municipality);
            return View(communities.ToList());
        }

        //
        // GET: /Admin/Community/Details/5

        public ActionResult Details(int id = 0)
        {
            Community community = db.Communities.Find(id);
            if (community == null)
            {
                return HttpNotFound();
            }
            return View(community);
        }

        //
        // GET: /Admin/Community/Create

        public ActionResult Create()
        {
            ViewBag.MunicipalID = new SelectList(db.Municipalities, "MunicipalID", "Name");
            return View();
        }

        //
        // POST: /Admin/Community/Create

        [HttpPost]
        public ActionResult Create(Community community)
        {
            if (ModelState.IsValid)
            {
                db.Communities.Add(community);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MunicipalID = new SelectList(db.Municipalities, "MunicipalID", "Name", community.MunicipalID);
            return View(community);
        }

        //
        // GET: /Admin/Community/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Community community = db.Communities.Find(id);
            if (community == null)
            {
                return HttpNotFound();
            }
            ViewBag.MunicipalID = new SelectList(db.Municipalities, "MunicipalID", "Name", community.MunicipalID);
            return View(community);
        }

        //
        // POST: /Admin/Community/Edit/5

        [HttpPost]
        public ActionResult Edit(Community community)
        {
            if (ModelState.IsValid)
            {
                db.Entry(community).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MunicipalID = new SelectList(db.Municipalities, "MunicipalID", "Name", community.MunicipalID);
            return View(community);
        }

        //
        // GET: /Admin/Community/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Community community = db.Communities.Find(id);
            if (community == null)
            {
                return HttpNotFound();
            }
            return View(community);
        }

        //
        // POST: /Admin/Community/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Community community = db.Communities.Find(id);
            db.Communities.Remove(community);
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