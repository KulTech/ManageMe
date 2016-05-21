using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ManageMeDomainEntity; 
using System.Web.Http.Description;

namespace ManageMeMobileService.Controllers
{
    public class StockController : ApiController
    {
        private ManageMeModel db = new ManageMeModel();
        [ResponseType(typeof(StockUser))]
        [HttpGet]
        public IHttpActionResult GetStocks(string username)
        {
            var p = db.StockUsers.Where(x => x.UserName == username); 
                if (p == null)
            {
                return NotFound();
            }

            return Ok(p);
        }
      
       [HttpPost]
        public string PostPeople(StockUser v)
        {
            try
            {
         
                db.StockUsers.Add(v);
                db.SaveChanges();
               
            }
         catch (Exception ex)
            {
                var x = new AppLog() { LogDate = DateTime.Now, msg = "inside save" + ex.Message + ex.InnerException.Message };
                db.AppLog.Add(x);
                db.SaveChanges();
                throw; 
            }
            return "{'Id':1,'UserName':'fengpan'}"; 
        }


    }
    }

   


