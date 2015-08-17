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
    public class BeneficiaryClassController : Controller
    {
        private CMSEntities db = new CMSEntities();

        //
        // GET: /Admin/BeneficiaryClass/

        public ActionResult Index()
        {
            return View(db.BeneficiaryClasses.ToList());
        }

        //
        // GET: /Admin/BeneficiaryClass/Details/5

        public ActionResult Details(int id = 0)
        {
            BeneficiaryClass beneficiaryclass = db.BeneficiaryClasses.Find(id);
            if (beneficiaryclass == null)
            {
                return HttpNotFound();
            }
            return View(beneficiaryclass);
        }

        //
        // GET: /Admin/BeneficiaryClass/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/BeneficiaryClass/Create

        [HttpPost]
        public ActionResult Create(BeneficiaryClass beneficiaryclass)
        {
            if (ModelState.IsValid)
            {
                db.BeneficiaryClasses.Add(beneficiaryclass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(beneficiaryclass);
        }

        //
        // GET: /Admin/BeneficiaryClass/Edit/5

        public ActionResult Edit(int id = 0)
        {
            BeneficiaryClass beneficiaryclass = db.BeneficiaryClasses.Find(id);
            if (beneficiaryclass == null)
            {
                return HttpNotFound();
            }
            return View(beneficiaryclass);
        }

        //
        // POST: /Admin/BeneficiaryClass/Edit/5

        [HttpPost]
        public ActionResult Edit(BeneficiaryClass beneficiaryclass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beneficiaryclass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(beneficiaryclass);
        }

        //
        // GET: /Admin/BeneficiaryClass/Delete/5

        public ActionResult Delete(int id = 0)
        {
            BeneficiaryClass beneficiaryclass = db.BeneficiaryClasses.Find(id);
            if (beneficiaryclass == null)
            {
                return HttpNotFound();
            }
            return View(beneficiaryclass);
        }

        //
        // POST: /Admin/BeneficiaryClass/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            BeneficiaryClass beneficiaryclass = db.BeneficiaryClasses.Find(id);
            db.BeneficiaryClasses.Remove(beneficiaryclass);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AssignProfileToCLass()
        {
            ViewBag.BeneficiaryClass = new SelectList(db.BeneficiaryClasses.ToList(), "BeneficiaryClassID", "Name");
            ViewBag.Profile = new SelectList(db.Profiles.ToList(), "ProfileID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AssignProfileToCLass(int BeneficiaryClass, int Profile)
        {
            if (ModelState.IsValid)
            {
                //BeneficiaryClass objClass = db.BeneficiaryClasses.Where(x => x.BeneficiaryClassID == BeneficiaryClass).FirstOrDefault();
                //Profile objProfile = db.Profiles.Where(x => x.ProfileID == Profile).FirstOrDefault();
                //List<Profile> objProfiles = new List<Profile>();
                //objProfiles.Add(objProfile);
                //objClass.Profile = objProfiles;
                //db.Entry(objClass).State = EntityState.Modified;
                //db.SaveChanges();
                BeneficiaryClass objClass = db.BeneficiaryClasses.Single(x => x.BeneficiaryClassID == BeneficiaryClass);
                Profile p = db.Profiles.Single(c => c.ProfileID == Profile);
                objClass.Profiles.Add(p);
                db.SaveChanges();

                ViewBag.BeneficiaryClass = new SelectList(db.BeneficiaryClasses.ToList(), "BeneficiaryClassID", "Name");
                ViewBag.Profile = new SelectList(db.Profiles.ToList(), "ProfileID", "Name");
            }
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}