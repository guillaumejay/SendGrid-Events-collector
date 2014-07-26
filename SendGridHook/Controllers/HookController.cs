using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using SendGridEventsModel;


namespace SendGridHook.Controllers
{
    public class HookController : ApiController
    {
        [HttpPost]
        [Route("Hook/Receive")]
        public async Task<HttpResponseMessage> Receive()
        {
            string jsonData = await Request.Content.ReadAsStringAsync();
            JArray dataList = JArray.Parse(jsonData);
           
            using (SendGridEventsDB context = new SendGridEventsDB())
            {
                foreach (JToken rec in dataList)
                {
             
                
                    Event newEvent = Event.CreateFromToken(rec);
                    context.Events.Add(newEvent);
                    context.SaveChanges();

                }
            }
            return new HttpResponseMessage(HttpStatusCode.OK);

        }

    }
}
