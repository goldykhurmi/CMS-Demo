using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMSProject.Areas.Admin.Models;

namespace CMSProject.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private CMSEntities db = new CMSEntities();
        //
        // GET: /Admin/Dashboard/

        public ActionResult Index()
        {
            ViewBag.TotalMembers = (db.Beneficiaries.Count());
            ViewBag.TotalSession = (db.Sessions.Count());
            return View();
        }

    }
}
