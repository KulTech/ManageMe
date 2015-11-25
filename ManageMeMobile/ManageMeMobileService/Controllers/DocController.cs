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

namespace ManageMeMobileService.Controllers
{
    public class DocController : ApiController
    {
        private ManageMeMobileDB db = new ManageMeMobileDB();

        // GET: api/Doc
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
        public IHttpActionResult PostDocuments(Documents documents)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Documents.Add(documents);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DocumentsExists(documents.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

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