using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMSProject.Areas.Admin.Models;
using System.IO;

namespace CMSProject.Areas.Admin.Controllers
{
    public class SessionAttachmentController : Controller
    {
        private CMSEntities db = new CMSEntities();

        //
        // GET: /Admin/SessionAttachment/

        public ActionResult Index()
        {
            var sessionattachments = db.SessionAttachments.Include(s => s.Session);
            return View(sessionattachments.ToList());
        }

        //
        // GET: /Admin/SessionAttachment/Details/5

        public ActionResult Details(int id = 0)
        {
            SessionAttachment sessionattachment = db.SessionAttachments.Find(id);
            if (sessionattachment == null)
            {
                return HttpNotFound();
            }
            return View(sessionattachment);
        }

        //
        // GET: /Admin/SessionAttachment/Create

        public ActionResult Create()
        {
            ViewBag.SessionID = new SelectList(db.Sessions, "SessionID", "Venue");
            return View();
        }

        //
        // POST: /Admin/SessionAttachment/Create

        [HttpPost]
        public ActionResult Create(IEnumerable<HttpPostedFileBase> files, SessionAttachment sessionattachment)
        {
            if (ModelState.IsValid)
            {
                foreach (var file in files)
                {
                    
                
                if (file == null)
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
                else if (file.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 3; //3 MB
                    string[] AllowedFileExtensions = new string[] { ".txt", ".docx", ".png", ".pdf" };

                    if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", AllowedFileExtensions));
                    }

                    else if (file.ContentLength > MaxContentLength)
                    {
                        ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                    }
                    else
                    {
                        //TO:DO


                        var sGuid = Guid.NewGuid().ToString();
                        var fileName = sGuid + file.FileName.Substring(file.FileName.LastIndexOf('.'));
                        var path = Path.Combine(Server.MapPath("~/Upload/SessionAttachments/"), fileName);
                        file.SaveAs(path);
                        ModelState.Clear();
                        ViewBag.Message = "File uploaded successfully";
                        sessionattachment.AttachedDocumentURL = "~/Upload/SessionAttachments/" + fileName;


                    }

                    if (ModelState.IsValid)
                    {
                        db.SessionAttachments.Add(sessionattachment);
                        db.SaveChanges();
                        
                    }

                }
                }
                return RedirectToAction("Index");
            }
                     

            ViewBag.SessionID = new SelectList(db.Sessions, "SessionID", "Venue", sessionattachment.SessionID);
            return View(sessionattachment);
        }

        //
        // GET: /Admin/SessionAttachment/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SessionAttachment sessionattachment = db.SessionAttachments.Find(id);
            if (sessionattachment == null)
            {
                return HttpNotFound();
            }
            ViewBag.SessionID = new SelectList(db.Sessions, "SessionID", "Venue", sessionattachment.SessionID);
            return View(sessionattachment);
        }

        //
        // POST: /Admin/SessionAttachment/Edit/5

        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file,SessionAttachment sessionattachment)
        {
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    //  ModelState.AddModelError("File", "Please Upload Your file");
                }
                else if (file.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 3; //3 MB
                    string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };

                    if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", AllowedFileExtensions));
                    }

                    else if (file.ContentLength > MaxContentLength)
                    {
                        ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                    }
                    else
                    {
                        //TO:DO
                        var sGuid = Guid.NewGuid().ToString();
                        var fileName = sGuid + file.FileName.Substring(file.FileName.LastIndexOf('.'));

                        var path = Path.Combine(Server.MapPath("~/Upload/SessionAttachments/"), fileName);
                        file.SaveAs(path);
                        ModelState.Clear();
                        ViewBag.Message = "File uploaded successfully";
                        sessionattachment.AttachedDocumentURL = "~/Upload/SessionAttachments/" + fileName;
                                            }
                           }
                
            if (ModelState.IsValid)
            {
                db.Entry(sessionattachment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SessionID = new SelectList(db.Sessions, "SessionID", "Venue", sessionattachment.SessionID);
            }
            return View(sessionattachment);
        }
 

        //
        // GET: /Admin/SessionAttachment/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SessionAttachment sessionattachment = db.SessionAttachments.Find(id);
            if (sessionattachment == null)
            {
                return HttpNotFound();
            }
            return View(sessionattachment);
        }

        //
        // POST: /Admin/SessionAttachment/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SessionAttachment sessionattachment = db.SessionAttachments.Find(id);
            db.SessionAttachments.Remove(sessionattachment);
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