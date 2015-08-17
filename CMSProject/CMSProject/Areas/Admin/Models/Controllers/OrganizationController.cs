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
    public class OrganizationController : Controller
    {
        private CMSEntities db = new CMSEntities();

        //
        // GET: /Admin/Organization/

        public ActionResult Index()
        {
            var organizations = db.Organizations.Include(o => o.Community);
            return View(organizations.ToList());
        }

        //
        // GET: /Admin/Organization/Details/5

        public ActionResult Details(int id = 0)
        {
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        //
        // GET: /Admin/Organization/Create

        public ActionResult Create()
        {
            ViewBag.CommunityID = new SelectList(db.Communities, "CommunityID", "Name");
            return View();
        }

        //
        // POST: /Admin/Organization/Create

        [HttpPost]
        public ActionResult Create(Organization organization)
        {
            if (ModelState.IsValid)
            {
                db.Organizations.Add(organization);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CommunityID = new SelectList(db.Communities, "CommunityID", "Name", organization.CommunityID);
            return View(organization);
        }

        //
        // GET: /Admin/Organization/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommunityID = new SelectList(db.Communities, "CommunityID", "Name", organization.CommunityID);
            return View(organization);
        }

        //
        // POST: /Admin/Organization/Edit/5

        [HttpPost]
        public ActionResult Edit(Organization organization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CommunityID = new SelectList(db.Communities, "CommunityID", "Name", organization.CommunityID);
            return View(organization);
        }

        //
        // GET: /Admin/Organization/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        //
        // POST: /Admin/Organization/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Organization organization = db.Organizations.Find(id);
            db.Organizations.Remove(organization);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AssignBeneficiaryToOrganization()
        {
            ViewBag.Beneficiary = new SelectList(db.Beneficiaries.ToList(), "BeneficiaryID", "Names");
            ViewBag.Organization = new SelectList(db.Organizations.ToList(), "OrganizationID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AssignBeneficiaryToOrganization(int Beneficiary, int Organization)
        {
            if (ModelState.IsValid)
            {
                Beneficiary objClass = db.Beneficiaries.Where(x => x.BeneficiaryID == Beneficiary).FirstOrDefault();
                Organization objOrganization = db.Organizations.Where(x => x.OrganizationID == Organization).FirstOrDefault();
                List<Organization> objOrganizations = new List<Organization>();
                objOrganizations.Add(objOrganization);
                objClass.Organizations = objOrganizations;
                db.Entry(objClass).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.Beneficiary = new SelectList(db.Beneficiaries.ToList(), "BeneficiaryID", "Names");
                ViewBag.Organization = new SelectList(db.Organizations.ToList(), "OrganizationID", "Name");
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