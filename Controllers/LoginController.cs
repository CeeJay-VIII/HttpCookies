using HttpCookies_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HttpCookies_Project.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// DATABASE
        /// </summary>
        LoginContext db = new LoginContext();


        // GET: Login
        public ActionResult Index()
        {
            try
            {
                //Retriev cookie data
                string emailAddress = Request.Cookies["user"].Value;

                if (emailAddress!=null)
                {
                    //Retrieve user data
                    Register register = db.Registers.Find(emailAddress);
                    if (!(register == null))
                    {
                        //Accesss granted

                        //Create cookie
                        HttpCookie userCookie = new HttpCookie("user", register.emailAddress);
                        //Set expiry
                        userCookie.Expires.AddMonths(1);
                        //Save cookie
                        HttpContext.Response.SetCookie(userCookie);

                        ViewBag.user = register.emailAddress;

                        return RedirectToAction("Index", "Registers");
                    }
                }
            }
            catch (Exception)
            {
                return View();
            }

            return View();
        }
        [HttpPost]
        public ActionResult Index(Login login)
        {
            try
            {
                Register register = db.Registers.Find(login.emailAddress);
                if(register.emailAddress==login.emailAddress && register.password == login.password)
                {
                    //Accesss granted

                    //Create cookie
                    HttpCookie userCookie = new HttpCookie("user", login.emailAddress);
                    //Set expiry
                    userCookie.Expires.AddMonths(1);
                    //Save cookie
                    HttpContext.Response.SetCookie(userCookie);

                    ViewBag.user = register.emailAddress;

                    return RedirectToAction("Index","Registers");
                }
            }
            catch (Exception ex)
            {
                ViewBag.feedback = ex.Message;
            }
            return View();
        }

        public ActionResult Logout()
        {
            HttpCookie cookie = Request.Cookies["user"];
            cookie.Expires = DateTime.Now;
            HttpContext.Response.SetCookie(cookie);
            return RedirectToAction("Index", "Home");
        }
    }
}