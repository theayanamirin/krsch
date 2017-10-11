using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    [Authorize(Users = "Admin")]
    public class RolesController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Roles/

        public ActionResult Index()
        {
            return View(db.webpages_Roles.ToList());
        }

        //
        // GET: /Roles/Details/5

        public ActionResult Details(int id = 0)
        {
            webpages_Roles webpages_roles = db.webpages_Roles.Find(id);
            if (webpages_roles == null)
            {
                return HttpNotFound();
            }
            return View(webpages_roles);
        }

        //
        // GET: /Roles/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(webpages_Roles webpages_roles)
        {
            if (ModelState.IsValid)
            {
                db.webpages_Roles.Add(webpages_roles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(webpages_roles);
        }

        //
        // GET: /Roles/Edit/5

        public ActionResult Edit(int id = 0)
        {
            webpages_Roles webpages_roles = db.webpages_Roles.Find(id);
            if (webpages_roles == null)
            {
                return HttpNotFound();
            }
            return View(webpages_roles);
        }

        //
        // POST: /Roles/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(webpages_Roles webpages_roles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(webpages_roles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(webpages_roles);
        }

        //
        // GET: /Roles/Delete/5

        public ActionResult Delete(int id = 0)
        {
            webpages_Roles webpages_roles = db.webpages_Roles.Find(id);
            if (webpages_roles == null)
            {
                return HttpNotFound();
            }
            return View(webpages_roles);
        }

        //
        // POST: /Roles/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            webpages_Roles webpages_roles = db.webpages_Roles.Find(id);
            db.webpages_Roles.Remove(webpages_roles);
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