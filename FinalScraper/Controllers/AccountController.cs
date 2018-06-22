using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalScraper.Models;

namespace FinalScraper.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.UserAccount.ToList());
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    db.UserAccount.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = account.Email + " Successfully Registered";
            }
            return View();
        }

        //login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            using (OurDbContext db = new OurDbContext())
            {
                var usr = db.UserAccount.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();
                if (usr != null)
                {
                    Session["UserID"] = usr.UserId.ToString();
                    Session["Email"] = usr.Email.ToString();
                    return RedirectToAction("Index", "stock");
                }
                else
                {
                    ModelState.AddModelError("", "Email or Password is incorrect");
                }
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("index", "home");
        }
    }
}