using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Script.Serialization;
using Shared.dto.Satellite;
using Shared;

namespace ClientWeb.Controllers
{
    public class WebActionController : Controller
    {
        // GET: WebAction
        public ActionResult Index()
        {
            return View();
        }

        public string GetUpdates()
        {
            string json = string.Empty;
            IList<Status> statuses = ClientWeb.Startup.GetStatus();

            if (statuses != null && statuses.Count > 0)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                serializer.Serialize(statuses);

                while (statuses.Count > 0)
                {
                    Status s = statuses.First();
                    statuses.Remove(s);
                    json = json + serializer.Serialize(s) + ",";
                }

                json = json.Substring(0, json.Length - 1);

                json = "[" + json + "]";
            }
            else
                json = ClientWebConstants.NO_READINGS;

            return json;
        }
    }
}