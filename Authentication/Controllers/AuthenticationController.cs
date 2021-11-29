using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Authentication.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(tbl_User user)
        {
            AuthenticationEntities db = new AuthenticationEntities();
            db.tbl_User.Add(user);
            db.SaveChanges();
            return RedirectToAction("UserList");
        }

        public ActionResult UserList()
        {
            AuthenticationEntities db = new AuthenticationEntities();
            var userList = db.tbl_User.ToList();
            return View(userList);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tbl_User objuser)
        {
            AuthenticationEntities db = new AuthenticationEntities();
            var user = db.tbl_User.Where(x => x.UserName == objuser.UserName && x.Password == objuser.Password).Count(); //FirstOrDefault() hole nicea (user !=null) ai condition ta hobe

            if (user > 0)
            {
                return RedirectToAction("UserList");
            }
            else
            {
                return View();
            }
        }

        public ActionResult UserProfile(int id)
        {
            AuthenticationEntities db = new AuthenticationEntities();
            var userlist = db.tbl_User.Find(id);
            return View(userlist);
        }

        [HttpPost]
        public ActionResult UserProfile(tbl_User objuser)
        {

            AuthenticationEntities db = new AuthenticationEntities();
            db.Entry(objuser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("UserList", new { id = objuser.UserId });
        }


    }
}