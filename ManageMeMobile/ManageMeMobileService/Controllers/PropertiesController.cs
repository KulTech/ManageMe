using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManageMeMobileService;

namespace ManageMeMobileService.Controllers
{
    public class PropertiesController : Controller
    {
        private ManageMeMobileDB db = new ManageMeMobileDB();

        // GET: Properties
        public ActionResult Index()
        {
            return View(db.Properties.ToList());
        }

        // GET: Properties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Properties properties = db.Properties.Find(id);
            if (properties == null)
            {
                return HttpNotFound();
            }
            return View(properties);
        }

        // GET: Properties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Properties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address1,City,State")] Properties properties)
        {
            if (ModelState.IsValid)
            {
                db.Properties.Add(properties);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(properties);
        }

        // GET: Properties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Properties properties = db.Properties.Find(id);
            if (properties == null)
            {
                return HttpNotFound();
            }
            return View(properties);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address1,City,State")] Properties properties)
        {
            if (ModelState.IsValid)
            {
                db.Entry(properties).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(properties);
        }

        // GET: Properties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Properties properties = db.Properties.Find(id);
            if (properties == null)
            {
                return HttpNotFound();
            }
            return View(properties);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Properties properties = db.Properties.Find(id);
            db.Properties.Remove(properties);
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
