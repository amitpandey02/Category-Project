using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CategoryNewProject.Models;

namespace CategoryNewProject.Controllers
{
    public class ReportController : Controller
    {
        DatabaseContext db=new DatabaseContext();
        // GET: Report
        [Authorize(Roles ="admin")]
        public ActionResult CreateReport()
        {
          var dataresult = db.Database.SqlQuery<ReportViewModel>("_spReport");                
          return View(dataresult);
        }
    }
}