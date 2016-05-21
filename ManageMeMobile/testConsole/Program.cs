using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageMeMobileService;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using ManageMeDomainEntity; 

namespace testConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // var d = new Documents() { Title = "testnew", Date = DateTime.Now.Date, Notes = "blab" };
            var d = new AppLog() { LogDate = DateTime.Now, msg = "test" }; 
            HttpWebRequest nReq = (HttpWebRequest)WebRequest.Create(@"https://managememobileservice.azurewebsites.net/api/Doc/PostVendors");

           
            nReq.Method = "POST";
            var v = new Vendors() { Name = "feng" }; 
            nReq.ContentType = "application/json";
            var productSerialized = JsonConvert.SerializeObject(v);
            using (StreamWriter sw = new StreamWriter(nReq.GetRequestStream()))
            {
                sw.Write(productSerialized);
                sw.Flush(); 
            }

            HttpWebResponse httpWebResponse = nReq.GetResponse() as HttpWebResponse;
            var test = httpWebResponse.GetResponseStream(); 
            using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                string x = sr.ReadToEnd(); 
            }
                
        }
    }
}
