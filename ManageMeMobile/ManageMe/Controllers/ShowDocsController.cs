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
    //[Authorize(Roles ="User")]
    public class ShowDocsController : Controller
    {
        private ManageMeModel db = new ManageMeModel();

        // GET: ShowDocs
        public ActionResult Index()
        {
                try
                {

                    return View(db.Documents.ToList().OrderBy(x => x.Property.Name));
                }
                catch (Exception ex)
                {
                    var a = new AppLog() { LogDate = DateTime.Now, msg = ex.Message + " " + ex.InnerException.Message };
                    db.AppLog.Add(a);
                    db.SaveChanges(); 
                    return View(); 
                }
            }

        // GET: ShowDocs/Details/5
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

        // GET: ShowDocs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShowDocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Date,Notes,fileContent")] Documents documents)
        {
            if (ModelState.IsValid)
            {
                db.Documents.Add(documents);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(documents);
        }

        // GET: ShowDocs/Edit/5
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
            var etypeList = from sub in db.SubTypes
                            join main in db.ExpenseTypes
                            on sub.ETypeId equals main.Id
                            select new 
                            {
                                eid = sub.Id,
                                TName = main.TypeName+"--"+sub.SubTypeName
                            }; 
            ViewBag.typeid = new SelectList(etypeList, "eid", "TName", documents.typeid);
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Name", documents.PropertyId);
            ViewBag.VendorId = new SelectList(db.Vendors, "Id", "Name", documents.VendorId);
            return View(documents);
        }

        // POST: ShowDocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Date,Notes,fileContent,PropertyId,amount,typeid,VendorId")] Documents documents)
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

        // GET: ShowDocs/Delete/5
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

        // POST: ShowDocs/Delete/5
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
