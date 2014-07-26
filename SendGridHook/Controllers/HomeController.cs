using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SendGridEventsModel;
using System.Net;

namespace SendGridHook.Controllers
{
    public class HomeController : Controller
    {
      
        public ActionResult Index()
        {
            SendGridEventsDB context = new SendGridEventsDB();

            List<Event> events = context.Events.OrderByDescending(x => x.EventDate).Take(50).ToList();

            return View(events);
        }
    }
}
