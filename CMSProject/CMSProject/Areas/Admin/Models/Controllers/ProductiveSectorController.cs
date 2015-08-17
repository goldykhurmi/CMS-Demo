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
    public class ProductiveSectorController : Controller
    {
        private CMSEntities db = new CMSEntities();

        //
        // GET: /Admin/ProductiveSector/

        public ActionResult Index()
        {
            return View(db.ProductiveSectors.ToList());
        }

        //
        // GET: /Admin/ProductiveSector/Details/5

        public ActionResult Details(int id = 0)
        {
            ProductiveSector productivesector = db.ProductiveSectors.Find(id);
            if (productivesector == null)
            {
                return HttpNotFound();
            }
            return View(productivesector);
        }

        //
        // GET: /Admin/ProductiveSector/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/ProductiveSector/Create

        [HttpPost]
        public ActionResult Create(ProductiveSector productivesector)
        {
            if (ModelState.IsValid)
            {
                db.ProductiveSectors.Add(productivesector);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productivesector);
        }

        //
        // GET: /Admin/ProductiveSector/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ProductiveSector productivesector = db.ProductiveSectors.Find(id);
            if (productivesector == null)
            {
                return HttpNotFound();
            }
            return View(productivesector);
        }

        //
        // POST: /Admin/ProductiveSector/Edit/5

        [HttpPost]
        public ActionResult Edit(ProductiveSector productivesector)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productivesector).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productivesector);
        }

        //
        // GET: /Admin/ProductiveSector/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ProductiveSector productivesector = db.ProductiveSectors.Find(id);
            if (productivesector == null)
            {
                return HttpNotFound();
            }
            return View(productivesector);
        }

        //
        // POST: /Admin/ProductiveSector/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductiveSector productivesector = db.ProductiveSectors.Find(id);
            db.ProductiveSectors.Remove(productivesector);
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