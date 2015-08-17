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
    public class SessionController : Controller
    {
        private CMSEntities db = new CMSEntities();

        //
        // GET: /Admin/Session/

        public ActionResult Index()
        {
            var sessions = db.Sessions.Include(s => s.BeneficiaryClass).Include(s => s.Project).Include(s => s.SessionStatus).Include(s => s.Teacher);
            return View(sessions.ToList());
        }

        //
        // GET: /Admin/Session/Details/5

        public ActionResult Details(int id = 0)
        {
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        //
        // GET: /Admin/Session/Create

        public ActionResult Create()
        {
            ViewBag.BeneficiaryClassID = new SelectList(db.BeneficiaryClasses, "BeneficiaryClassID", "Name");
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "Name");
            ViewBag.SessionStatusID = new SelectList(db.SessionStatuses, "SessionStatusID", "Name");
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "Name");
            return View();
        }

        //
        // POST: /Admin/Session/Create

        [HttpPost]
        public ActionResult Create(Session session)
        {
            if (ModelState.IsValid)
            {
                db.Sessions.Add(session);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BeneficiaryClassID = new SelectList(db.BeneficiaryClasses, "BeneficiaryClassID", "Name", session.BeneficiaryClassID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "Name", session.ProjectID);
            ViewBag.SessionStatusID = new SelectList(db.SessionStatuses, "SessionStatusID", "Name", session.SessionStatusID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "Name", session.TeacherID);
            return View(session);
        }

        //
        // GET: /Admin/Session/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            ViewBag.BeneficiaryClassID = new SelectList(db.BeneficiaryClasses, "BeneficiaryClassID", "Name", session.BeneficiaryClassID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "Name", session.ProjectID);
            ViewBag.SessionStatusID = new SelectList(db.SessionStatuses, "SessionStatusID", "Name", session.SessionStatusID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "Name", session.TeacherID);
            return View(session);
        }

        //
        // POST: /Admin/Session/Edit/5

        [HttpPost]
        public ActionResult Edit(Session session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BeneficiaryClassID = new SelectList(db.BeneficiaryClasses, "BeneficiaryClassID", "Name", session.BeneficiaryClassID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "Name", session.ProjectID);
            ViewBag.SessionStatusID = new SelectList(db.SessionStatuses, "SessionStatusID", "Name", session.SessionStatusID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "Name", session.TeacherID);
            return View(session);
        }

        //
        // GET: /Admin/Session/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        //
        // POST: /Admin/Session/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Session session = db.Sessions.Find(id);
            db.Sessions.Remove(session);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AssignBeneficiaryToSession()
        {
            ViewBag.Beneficiary = new SelectList(db.Beneficiaries.ToList(), "BeneficiaryID", "Names");
            ViewBag.Session = new SelectList(db.Sessions.ToList(), "SessionID", "SessionID");
            return View();
        }

        [HttpPost]
        public ActionResult AssignBeneficiaryToSession(int Beneficiary, int Session, bool Attended, bool Completed)
        {
            if (ModelState.IsValid)
            {
                //Beneficiary objClass = db.Beneficiaries.Where(x => x.BeneficiaryID == Beneficiary).FirstOrDefault();
                //Session objSession = db.Sessions.Where(x => x.SessionID == Session).FirstOrDefault();
                //List<Session> objSessions = new List<Session>();
                //objSessions.Add(objSession);
                //objClass.Session = objSessions;
                //db.Entry(objClass).State = EntityState.Modified;
                //db.SaveChanges();

                SessionParticipant ObjSP = new SessionParticipant();
                ObjSP.SessionID = Session;
                ObjSP.BeneficiaryID = Beneficiary;
                ObjSP.Attended = Attended;
                ObjSP.Completed = Completed;
                db.SessionParticipants.Add(ObjSP);
                db.SaveChanges();
               

                //Beneficiary objClass = db.Beneficiaries.Single(x => x.BeneficiaryID == Beneficiary);
                //Session p = db.Sessions.Single(c => c.SessionID == Session);
                //objClass.Session.Add(p);
                //db.SaveChanges();

                ViewBag.Beneficiary = new SelectList(db.Beneficiaries.ToList(), "BeneficiaryID", "Names");
                ViewBag.Session = new SelectList(db.Sessions.ToList(), "SessionID", "SessionID");
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