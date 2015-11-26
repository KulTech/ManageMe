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
    
    public class AppLogsController : ApiController
    {
        private ManageMeMobileDB db = new ManageMeMobileDB();

        // GET: api/AppLogs
        public IQueryable<AppLog> GetAppLog()
        {
            return db.AppLog;
        }

        // GET: api/AppLogs/5
        [ResponseType(typeof(AppLog))]
        public IHttpActionResult GetAppLog(int id)
        {
            AppLog appLog = db.AppLog.Find(id);
            if (appLog == null)
            {
                return NotFound();
            }

            return Ok(appLog);
        }

        // PUT: api/AppLogs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppLog(int id, AppLog appLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appLog.Id)
            {
                return BadRequest();
            }

            db.Entry(appLog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppLogExists(id))
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

        // POST: api/AppLogs
        [ResponseType(typeof(AppLog))]
        public IHttpActionResult PostAppLog(AppLog appLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AppLog.Add(appLog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = appLog.Id }, appLog);
        }

        // DELETE: api/AppLogs/5
        [ResponseType(typeof(AppLog))]
        public IHttpActionResult DeleteAppLog(int id)
        {
            AppLog appLog = db.AppLog.Find(id);
            if (appLog == null)
            {
                return NotFound();
            }

            db.AppLog.Remove(appLog);
            db.SaveChanges();

            return Ok(appLog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppLogExists(int id)
        {
            return db.AppLog.Count(e => e.Id == id) > 0;
        }
    }
}