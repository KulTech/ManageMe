using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ManageMeMobileService;
using ManageMeMobileService.Helper;

namespace ManageMeMobileService.Controllers
{
    public class DocController : ApiController
    {
        private ManageMeMobileDB db = new ManageMeMobileDB();

        // GET: api/Doc
        [HttpGet]
        public IQueryable<Properties> GetProperties()
        {
            return db.Properties; 
        }

        public IQueryable<Documents> GetDocuments()
        {
            return db.Documents;
        }

        // GET: api/Doc/5
        [ResponseType(typeof(Documents))]
        public IHttpActionResult GetDocuments(int id)
        {
            Documents documents = db.Documents.Find(id);
            if (documents == null)
            {
                return NotFound();
            }

            return Ok(documents);
        }

        // PUT: api/Doc/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDocuments(int id, Documents documents)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != documents.Id)
            {
                return BadRequest();
            }

            db.Entry(documents).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Doc
        [ResponseType(typeof(Documents))]
        public IHttpActionResult PostDocuments(DocumentsViewModel documents)
        {
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
            Documents d = new Documents();
            //var a = new AppLog() { logDate = DateTime.Now, msg = "before save" };
            //db.AppLog.Add(a);
            //db.SaveChanges();
            try
            {
           
            d.Title = documents.Title;
            d.Notes = documents.Notes;
            d.Date = documents.Date;
                d.PropertyId = documents.PropertyId; 
            string base64 = documents.fileContent.Replace("data:image/jpeg;base64,","");// load base 64 code to this variable from js 
            Byte[] bitmapData = new Byte[base64.Length];
            bitmapData = Convert.FromBase64String(FixBase64Helper.FixBase64ForImage(base64));
            d.fileContent =bitmapData;
                //var w = new AppLog() { logDate = DateTime.Now, msg = "inside save" };
                //db.AppLog.Add(w);
                //db.SaveChanges();

                db.Documents.Add(d);
                db.SaveChanges(); 

            }
            catch (Exception ex)
            {
                if (DocumentsExists(documents.Id))
                {
                    return Conflict();
                }
                else
                {
                    db.Documents.Remove(d); 
                    var log = new AppLog() { logDate = DateTime.Now, msg = ex.Message };
                    db.AppLog.Add(log);
                    db.SaveChanges();
                throw; 
                    //return Conflict(); 
                }
            }
            //var b = new AppLog() { logDate = DateTime.Now, msg = "after save" };
            //db.AppLog.Add(b);
            //db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = documents.Id }, documents);
           
        }

        // DELETE: api/Doc/5
        [ResponseType(typeof(Documents))]
        public IHttpActionResult DeleteDocuments(int id)
        {
            Documents documents = db.Documents.Find(id);
            if (documents == null)
            {
                return NotFound();
            }

            db.Documents.Remove(documents);
            db.SaveChanges();

            return Ok(documents);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DocumentsExists(int id)
        {
            return db.Documents.Count(e => e.Id == id) > 0;
        }
    }
}