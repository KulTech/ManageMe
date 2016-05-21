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
using System.Web.Mvc;
using Newtonsoft.Json; 

namespace ManageMeMobileService.Controllers
{
    public class StockTestController : Controller
    {
   
        public String PostPeople(StockUser u)
        {
            return JsonConvert.SerializeObject(new StockUser() { Id = 1, UserName = "fengpan" }); 
        }
    }
}
