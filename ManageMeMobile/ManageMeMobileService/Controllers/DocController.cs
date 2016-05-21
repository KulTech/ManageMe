using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.IO; 
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ManageMeMobileService;
using ManageMeMobileService.Helper;
using ManageMeDomainEntity;
using ManageMeMobileService.ViewModel;
using System.Net.Mail;

namespace ManageMeMobileService.Controllers
{
    public class DocController : ApiController
    {
        private ManageMeModel db = new ManageMeModel();

        // GET: api/Doc
        [HttpGet]
        public IQueryable<Properties> GetProperties()
        {
                
                return db.Properties;
        }
        [HttpGet]
        public ICollection<ExpenseTypeViewModel> GetETypes()
        {
            AutoMapper.Mapper.CreateMap<ExpenseType, ExpenseTypeViewModel>();
            var d = AutoMapper.Mapper.Map<ICollection<ExpenseType>, ICollection<ExpenseTypeViewModel>>(db.ExpenseTypes.ToList());
            return d ; 
        }
        [HttpGet]
        public IQueryable<SubTypes> GetSubTypes(int Id)
        {
            return db.SubTypes.Where(x => x.ETypeId == Id).OrderBy(x=>x.SubTypeName); 
        }

        [HttpGet]
        public IQueryable<Vendors> GetVendors()
        {
            return db.Vendors.OrderBy(x=>x.Name);
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
        public int PostDocuments(DocumentsViewModel documents)
        {

            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }

            try
            {
                AutoMapper.Mapper.CreateMap<DocumentsViewModel, Documents>();
                var d = AutoMapper.Mapper.Map<DocumentsViewModel, Documents>(documents);
                if (d.fileContent != null && d.fileContent.Length > 0)
                {
                    string base64 = documents.fileContent.Replace("data:image/jpeg;base64,", "");// load base 64 code to this variable from js 
                    Byte[] bitmapData = new Byte[base64.Length];
                    bitmapData = Convert.FromBase64String(FixBase64Helper.FixBase64ForImage(base64));
                    d.fileContent = bitmapData;
                }
                if (documents.Id != 0) 
                {
                    db.Documents.Attach(d);
                    db.Entry(d).State = EntityState.Modified; 
                }
                else
                {
                    db.Documents.Add(d);  
                }
                db.SaveChanges();
                return d.Id;
                //var client = new SmtpClient("smtp.gmail.com", 587)
                //{
                //    Credentials = new NetworkCredential("wadepan@gmail.com", "pflm74616"),
                //    EnableSsl = true
                //};
                ////MailAddress from = new MailAddress("manageme@kultech.com");
                ////MailAddress to = new MailAddress("wadepan@gmail.com");
                ////MailAddress cc = new MailAddress("pliming@gmail.com");
                ////MailMessage msg = new MailMessage("manageme@kultech.com", "wadepan@gmail.com", "New ManageMe Document: " + documents.Notes, "test");
                ////msg.To.Add(cc);
                //////Stream s = null;
                //////var writer = new BinaryWriter(s);
                //////writer.Write(documents.fileContent);
                //////Attachment a = new Attachment(s, documents.Notes + ".jpeg");
                ////msg.Body = "Type: " + d.SubType.SubTypeName;
                //////msg.Attachments.Add(a);
                ////client.Send(msg);
            }
            catch (Exception ex)
            {
                    //db.Documents.Remove(db.Documents.Where(x => x.Id == documents.Id).FirstOrDefault());  
                    var log = new AppLog() { LogDate = DateTime.Now, msg = ex.InnerException.Message };
                    db.AppLog.Add(log);
                    db.SaveChanges();
                throw; 
                    //return Conflict(); 
                
            }
            //var b = new AppLog() { logDate = DateTime.Now, msg = "after save" };
            //db.AppLog.Add(b);
            //db.SaveChanges(); 
        }
        [ResponseType(typeof(Vendors))]
        public IHttpActionResult PostVendors(Vendors v)
        {
            try
            {
                db.Vendors.Add(v);
                db.SaveChanges();
             
            }
            catch(Exception ex)
            {
                var x = new AppLog() { LogDate = DateTime.Now, msg = "inside save"+ex.Message+ex.InnerException.Message };
                db.AppLog.Add(x);
                db.SaveChanges();
             
            }
            return CreatedAtRoute("DefaultApi", new { id = v.Id }, v);
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