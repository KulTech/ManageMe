using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManageMeDomainEntity;

namespace ManageMe.Controllers
{
    public class fengDocumentsController : Controller
    {
        private ManageMeModel db = new ManageMeModel();

        // GET: fengDocuments
        public ActionResult Index()
        {
            var documents = db.Documents.Include(d => d.SubType).Include(d => d.Property);
            return View(documents.ToList());
        }

        // GET: fengDocuments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documents documents = db.Documents.Find(id);
            if (documents == null)
            {
                return HttpNotFound();
            }
            return View(documents);
        }

        // GET: fengDocuments/Create
        public ActionResult Create()
        {
            ViewBag.typeid = new SelectList(db.ExpenseTypes, "Id", "TypeName");
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Name");
            return View();
        }

        // POST: fengDocuments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Date,Notes,fileContent,PropertyId,amount,typeid")] Documents documents)
        {
            if (ModelState.IsValid)
            {
                db.Documents.Add(documents);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.typeid = new SelectList(db.ExpenseTypes, "Id", "TypeName", documents.typeid);
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Name", documents.PropertyId);
            return View(documents);
        }

        // GET: fengDocuments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documents documents = db.Documents.Find(id);
            if (documents == null)
            {
                return HttpNotFound();
            }
            ViewBag.typeid = new SelectList(db.ExpenseTypes, "Id", "TypeName", documents.typeid);
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Name", documents.PropertyId);
            return View(documents);
        }

        // POST: fengDocuments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Date,Notes,fileContent,PropertyId,amount,typeid")] Documents documents)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documents).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.typeid = new SelectList(db.ExpenseTypes, "Id", "TypeName", documents.typeid);
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Name", documents.PropertyId);
            return View(documents);
        }

        // GET: fengDocuments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documents documents = db.Documents.Find(id);
            if (documents == null)
            {
                return HttpNotFound();
            }
            return View(documents);
        }

        // POST: fengDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Documents documents = db.Documents.Find(id);
            db.Documents.Remove(documents);
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
