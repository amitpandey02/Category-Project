using CategoryNewProject.Migrations;
using CategoryNewProject.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
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
        //public ActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Login(Credential cr)
        //{
        //    var isValid = db.Credentials.Any(x => x.UserName == cr.UserName && x.Password == cr.Password);

        //    if (isValid)
        //    {
        //        FormsAuthentication.SetAuthCookie(cr.UserName, false);
        //        return RedirectToAction("CategoryList", "Category");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}
        //public ActionResult Logout()
        //{
        //    Session.Abandon();
        //    FormsAuthentication.SignOut();
        //    return RedirectToAction("Login");

        //}
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

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Credential cr)
        {
            var isValid = db.Credentials.Any(x => x.UserName == cr.UserName && x.Password == cr.Password);

            var parameters = new[]
            {
                new SqlParameter("@username",cr.UserName),
                new SqlParameter("@userpassword",cr.Password)
            };



            var data = db.Database.SqlQuery<RoleViewModel>("exec _spLogin @username,@userpassword", parameters).FirstOrDefault(); 
            if (isValid)
            {
                Role obj = new Role();
                obj.UserRole=data.UserRole;
                var token = JwtAuthentication.CreateJWTToken(cr,obj);
                Response.Cookies.Set(new HttpCookie("token", token));
                return RedirectToAction("CategoryList", "Category");
            }
            else
            {
                return View();
            }
          }
        public ActionResult Logout()
        {
            if (Request.Cookies["token"] != null)
            {
                var c = new HttpCookie("token")
                {
                    Expires = DateTime.Now.AddSeconds(1)
                };
                Response.Cookies.Add(c);
            }
            return RedirectToAction("Login");

        }


    }
}