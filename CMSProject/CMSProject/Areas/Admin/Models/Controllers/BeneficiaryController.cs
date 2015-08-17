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
    public class BeneficiaryController : Controller
    {
        private CMSEntities db = new CMSEntities();

        //
        // GET: /Admin/Beneficiary/

        public ActionResult Index()
        {
            var beneficiaries = db.Beneficiaries.Include(b => b.Community).Include(b => b.TypeOfBeneficiary);
            return View(beneficiaries.ToList());
        }

        //
        // GET: /Admin/Beneficiary/Details/5

        public ActionResult Details(int id = 0)
        {
            Beneficiary beneficiary = db.Beneficiaries.Find(id);
            if (beneficiary == null)
            {
                return HttpNotFound();
            }
            return View(beneficiary);
        }

        //
        // GET: /Admin/Beneficiary/Create

        public ActionResult Create()
        {
            ViewBag.CommunityID = new SelectList(db.Communities, "CommunityID", "Name");
            ViewBag.TypeOfBeneficiaryID = new SelectList(db.TypeOfBeneficiaries, "TypeOfBeneficiaryID", "Description");
            return View();
        }

        //
        // POST: /Admin/Beneficiary/Create

        [HttpPost]
        public ActionResult Create(Beneficiary beneficiary)
        {
            if (ModelState.IsValid)
            {
                db.Beneficiaries.Add(beneficiary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CommunityID = new SelectList(db.Communities, "CommunityID", "Name", beneficiary.CommunityID);
            ViewBag.TypeOfBeneficiaryID = new SelectList(db.TypeOfBeneficiaries, "TypeOfBeneficiaryID", "Description", beneficiary.TypeOfBeneficiaryID);
            return View(beneficiary);
        }

        //
        // GET: /Admin/Beneficiary/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Beneficiary beneficiary = db.Beneficiaries.Find(id);
            if (beneficiary == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommunityID = new SelectList(db.Communities, "CommunityID", "Name", beneficiary.CommunityID);
            ViewBag.TypeOfBeneficiaryID = new SelectList(db.TypeOfBeneficiaries, "TypeOfBeneficiaryID", "Description", beneficiary.TypeOfBeneficiaryID);
            return View(beneficiary);
        }

        //
        // POST: /Admin/Beneficiary/Edit/5

        [HttpPost]
        public ActionResult Edit(Beneficiary beneficiary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beneficiary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CommunityID = new SelectList(db.Communities, "CommunityID", "Name", beneficiary.CommunityID);
            ViewBag.TypeOfBeneficiaryID = new SelectList(db.TypeOfBeneficiaries, "TypeOfBeneficiaryID", "Description", beneficiary.TypeOfBeneficiaryID);
            return View(beneficiary);
        }

        //
        // GET: /Admin/Beneficiary/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Beneficiary beneficiary = db.Beneficiaries.Find(id);
            if (beneficiary == null)
            {
                return HttpNotFound();
            }
            return View(beneficiary);
        }

        //
        // POST: /Admin/Beneficiary/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Beneficiary beneficiary = db.Beneficiaries.Find(id);
            db.Beneficiaries.Remove(beneficiary);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BeneficiaryToSector()
        {
            ViewBag.Beneficiary = new SelectList(db.Beneficiaries.ToList(), "BeneficiaryID", "Names");
            ViewBag.ProductiveSector = new SelectList(db.ProductiveSectors.ToList(), "SectorId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult BeneficiaryToSector(int Beneficiary, int ProductiveSector)
        {
            if (ModelState.IsValid)
            {
                //Beneficiary objClass = db.Beneficiaries.Where(x => x.BeneficiaryID == Beneficiary).FirstOrDefault();
                //ProductiveSector objProductiveSector = db.ProductiveSectors.Where(x => x.SectorId == ProductiveSector).FirstOrDefault();
                //List<ProductiveSector> objProductiveSectors = new List<ProductiveSector>();
                //objProductiveSectors.Add(objProductiveSector);
                //objClass.ProductiveSectors = objProductiveSectors;
                //db.Entry(objClass).State = EntityState.Modified;
                //db.SaveChanges();

                //var ctx = new CMSEntities();

                Beneficiary objClass = db.Beneficiaries.Single(x => x.BeneficiaryID == Beneficiary);
                ProductiveSector p = db.ProductiveSectors.Single(c => c.SectorId == ProductiveSector);
                objClass.ProductiveSectors.Add(p);
                db.SaveChanges();
               
                ViewBag.Beneficiary = new SelectList(db.Beneficiaries.ToList(), "BeneficiaryID", "Names");
                ViewBag.ProductiveSector = new SelectList(db.ProductiveSectors.ToList(), "SectorId", "Name");
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