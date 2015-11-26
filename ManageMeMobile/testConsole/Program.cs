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

namespace testConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var d = new Documents() { Title = "testnew", Date = DateTime.Now.Date, Notes = "blab" };
            HttpWebRequest nReq = (HttpWebRequest)WebRequest.Create(@"http://managememobileservice.azurewebsites.net/api/Doc");
           
            nReq.Method = "POST";
            nReq.ContentType = "application/json";
            var productSerialized = JsonConvert.SerializeObject(d);
            using (StreamWriter sw = new StreamWriter(nReq.GetRequestStream()))
            {
                sw.Write(productSerialized);
            }
            HttpWebResponse httpWebResponse = nReq.GetResponse() as HttpWebResponse;
            using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
            { }
                
        }
    }
}
