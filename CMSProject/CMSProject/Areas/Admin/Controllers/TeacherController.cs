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
    public class TeacherController : Controller
    {
        private CMSEntities db = new CMSEntities();

        //
        // GET: /Admin/Teacher/

        public ActionResult Index()
        {
            ViewBag.CurrentPage = "Teachers";
            return View(db.Teachers.ToList());
        }

        //
        // GET: /Admin/Teacher/Details/5

        public ActionResult Details(int id = 0)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        //
        // GET: /Admin/Teacher/Create

        public ActionResult Create()
        {
            ViewBag.CurrentPage = "Create Teachers";
            return View();
        }

        //
        // POST: /Admin/Teacher/Create

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
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


                        var path = Path.Combine(Server.MapPath("~/Upload/TeacherImages/"), fileName);
                        file.SaveAs(path);
                        ModelState.Clear();
                        ViewBag.Message = "File uploaded successfully";
                        teacher.Photo = "~/Upload/TeacherImages/" + fileName;

                    }

                    if (ModelState.IsValid)
                    {
                        db.Teachers.Add(teacher);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }


                }


            }
            return View(teacher);
        }

        //
        // GET: /Admin/Teacher/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        //
        // POST: /Admin/Teacher/Edit/5

        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, Teacher Edit)
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

                        var path = Path.Combine(Server.MapPath("~/Upload/TeacherImages/"), fileName);
                        file.SaveAs(path);
                        ModelState.Clear();
                        ViewBag.Message = "File uploaded successfully";
                        Edit.Photo = "~/Upload/TeacherImages/" + fileName;

                    }
                }
                if (ModelState.IsValid)
                {
                    db.Entry(Edit).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return View(Edit);
        }

        //
        // GET: /Admin/Teacher/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        //
        // POST: /Admin/Teacher/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
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