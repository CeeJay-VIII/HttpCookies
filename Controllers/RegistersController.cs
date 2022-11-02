using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HttpCookies_Project.Models;

namespace HttpCookies_Project.Controllers
{
    public class RegistersController : Controller
    {
        private LoginContext db = new LoginContext();

        // GET: Registers
        public ActionResult Index()
        {
            return View(db.Registers.ToList());
        }

        // GET: Registers/Details/5
        public ActionResult Details(string emailAddress)
        {
            if (emailAddress == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register register = db.Registers.Find(emailAddress);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // GET: Registers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Registers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "emailAddress,userId,name,password,confirmPassword")] Register register)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Registers.Add(register);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Login");
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return View(register);
        }

        // GET: Registers/Edit/5
        public ActionResult Edit(string emailAddress)
        {
            if (emailAddress == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register register = db.Registers.Find(emailAddress);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // POST: Registers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "emailAddress,userId,name,password,confirmPassword")] Register register)
        {
            try
            {
                register.confirmPassword = register.password;
                db.Entry(register).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Registers/Delete/5
        public ActionResult Delete(string emailAddress)
        {
            if (emailAddress == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register register = db.Registers.Find(emailAddress);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // POST: Registers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string emailAddress)
        {
            Register register = db.Registers.Find(emailAddress);
            db.Registers.Remove(register);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
