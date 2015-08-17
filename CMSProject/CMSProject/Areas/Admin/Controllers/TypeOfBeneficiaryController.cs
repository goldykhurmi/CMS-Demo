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
    public class TypeOfBeneficiaryController : Controller
    {
        private CMSEntities db = new CMSEntities();

        //
        // GET: /Admin/TypeOfBeneficiary/

        public ActionResult Index()
        {
            return View(db.TypeOfBeneficiaries.ToList());
        }

        //
        // GET: /Admin/TypeOfBeneficiary/Details/5

        public ActionResult Details(int id = 0)
        {
            TypeOfBeneficiary typeofbeneficiary = db.TypeOfBeneficiaries.Find(id);
            if (typeofbeneficiary == null)
            {
                return HttpNotFound();
            }
            return View(typeofbeneficiary);
        }

        //
        // GET: /Admin/TypeOfBeneficiary/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/TypeOfBeneficiary/Create

        [HttpPost]
        public ActionResult Create(TypeOfBeneficiary typeofbeneficiary)
        {
            if (ModelState.IsValid)
            {
                db.TypeOfBeneficiaries.Add(typeofbeneficiary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeofbeneficiary);
        }

        //
        // GET: /Admin/TypeOfBeneficiary/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TypeOfBeneficiary typeofbeneficiary = db.TypeOfBeneficiaries.Find(id);
            if (typeofbeneficiary == null)
            {
                return HttpNotFound();
            }
            return View(typeofbeneficiary);
        }

        //
        // POST: /Admin/TypeOfBeneficiary/Edit/5

        [HttpPost]
        public ActionResult Edit(TypeOfBeneficiary typeofbeneficiary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeofbeneficiary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeofbeneficiary);
        }

        //
        // GET: /Admin/TypeOfBeneficiary/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TypeOfBeneficiary typeofbeneficiary = db.TypeOfBeneficiaries.Find(id);
            if (typeofbeneficiary == null)
            {
                return HttpNotFound();
            }
            return View(typeofbeneficiary);
        }

        //
        // POST: /Admin/TypeOfBeneficiary/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeOfBeneficiary typeofbeneficiary = db.TypeOfBeneficiaries.Find(id);
            db.TypeOfBeneficiaries.Remove(typeofbeneficiary);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AssignTypeBeneficiaryToProfile()
        {
            ViewBag.TypeOfBeneficiary = new SelectList(db.TypeOfBeneficiaries.ToList(), "TypeOfBeneficiaryID", "Description");
            ViewBag.Profile = new SelectList(db.Profiles.ToList(), "ProfileID", "Name");
            return View();
        }

       [HttpPost]
        public ActionResult AssignTypeBeneficiaryToProfile(int TypeOfBeneficiary, int Profile)
        {
            if (ModelState.IsValid)
            {
               //TypeOfBeneficiary objClass = db.TypeOfBeneficiaries.Where(x => x.TypeOfBeneficiaryID == TypeOfBeneficiary).FirstOrDefault();
               // Profile objProfile = db.Profiles.Where(x => x.ProfileID == Profile).FirstOrDefault();
               // List<Profile> objProfiles = new List<Profile>();
               // objProfiles.Add(objProfile);
               // objClass.Profiles = objProfiles;
               // db.Entry(objClass).State = EntityState.Modified;
               // db.SaveChanges();
                TypeOfBeneficiary objClass = db.TypeOfBeneficiaries.Single(x => x.TypeOfBeneficiaryID == TypeOfBeneficiary);
                Profile p = db.Profiles.Single(c => c.ProfileID == Profile);
                objClass.Profiles.Add(p);
                db.SaveChanges();

                ViewBag.TypeOfBeneficiary = new SelectList(db.TypeOfBeneficiaries.ToList(), "TypeOfBeneficiaryID", "Description");
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