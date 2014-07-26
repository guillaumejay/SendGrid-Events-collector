using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SendGridEventsModel
{
	public partial class Event
	{
		public Event()
		{
		}

		public Event(long timeStamp, string email, string sgEvent, string category)
		{
			EventDate = UnixTimeStampToDateTime(timeStamp);
			Email = email;
			this.Event1 = sgEvent;
			Category = category;
		}

		private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
		{
			// Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dtDateTime;
		}

		private static List<string> knownFieldList = new List<string>
		{

                "email",
                "sg_event_id",
                "sg_message_id",
                "timestamp",
                "smtp-id",
                "event",
                "category",
                "reason",
                "useragent",
                "JobID",
                "ip", "url", "status", "type"
		};
		public static Event CreateFromToken(JToken token)
		{
			// common parameters
            string eventName = (string)token["event"];

			string smtpID = (string)token["smtp-id"] ?? "";
			string email = (string)token["email"];
			long timeStamp = (long)token["timestamp"];
			var categories = token["category"];
			string category = null;
			if (categories!=null)
			{
				category = String.Join(",", categories);
			}
			
            Event newEvent  = new Event(timeStamp, email, eventName, category);
            newEvent.Response = (string)token["response"] ?? "";
            newEvent.Attempt = (int?)token["attempt"] ?? 0;
            newEvent.smtpID = smtpID;
            //if (token["JobID"] == null)
            //{
            //    throw new ArgumentNullException("JobID is missing");
            //}
            newEvent.JobID = (int)( token["JobID"]??0);
            
            newEvent.Reason = (string)token["reason"] ?? "";
            newEvent.IP = (string)token["ip"] ?? "";
            newEvent.UserAgent = (string)token["useragent"] ?? "";
            newEvent.URL = (string)token["url"] ?? "";
            newEvent.Status = (string)token["status"] ?? "";
            newEvent.BounceType = (string)token["type"] ?? "";
            List<String> uniqueList=new List<string>();
            foreach (JToken specific in token)
            {
                JProperty property = ((JProperty)specific);

                if (!knownFieldList.Contains( property.Name))
                {
                    uniqueList.Add(specific.ToString());
                }
            }
            uniqueList.Sort();
		    if (uniqueList.Count > 0)
		    {
		        newEvent.UniqueArguments = String.Join(",", uniqueList);
		    }
		    return newEvent;
        }
    }
}