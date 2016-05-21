using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using ManageMeMobileService;
using ManageMeDomainEntity; 

namespace ManageMeMobileService.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using ManageMeMobileService;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Documents>("Documents");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class DocumentsController : ODataController
    {
        private ManageMeModel db = new ManageMeModel();

        // GET: odata/Documents
        [EnableQuery]
        public IQueryable<Documents> GetDocuments()
        {
            return db.Documents;
        }

        // GET: odata/Documents(5)
        [EnableQuery]
        public SingleResult<Documents> GetDocuments([FromODataUri] int key)
        {
            return SingleResult.Create(db.Documents.Where(documents => documents.Id == key));
        }

        // PUT: odata/Documents(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Documents> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Documents documents = await db.Documents.FindAsync(key);
            if (documents == null)
            {
                return NotFound();
            }

            patch.Put(documents);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentsExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(documents);
        }

        // POST: odata/Documents
        public async Task<IHttpActionResult> Post(Documents documents)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Documents.Add(documents);

            try
            {
                await db.SaveChangesAsync();
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

            return Created(documents);
        }

        // PATCH: odata/Documents(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Documents> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Documents documents = await db.Documents.FindAsync(key);
            if (documents == null)
            {
                return NotFound();
            }

            patch.Patch(documents);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentsExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(documents);
        }

        // DELETE: odata/Documents(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Documents documents = await db.Documents.FindAsync(key);
            if (documents == null)
            {
                return NotFound();
            }

            db.Documents.Remove(documents);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DocumentsExists(int key)
        {
            return db.Documents.Count(e => e.Id == key) > 0;
        }
    }
}
