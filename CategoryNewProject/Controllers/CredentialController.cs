using CategoryNewProject.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CategoryNewProject.Controllers
{
    public class CredentialController : Controller
    {
        DatabaseContext db=new DatabaseContext();
        // GET: Credential
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Credential cr)
        {
            var isValid = db.Credentials.Any(x => x.UserName == cr.UserName && x.Password == cr.Password);

            if (isValid)
            {
                FormsAuthentication.SetAuthCookie(cr.UserName, false);
                return RedirectToAction("CategoryList", "Category");
            }
            else
            {
                return View();
            }
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SignUp(Credential cr)
        {
            db.Credentials.Add(cr);
            await db.SaveChangesAsync();
            return RedirectToAction("Login");

        }
        
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");

        }
    }
}