using Microsoft.Owin;
using Owin;

using Microsoft.AspNet.SignalR.Client.Hubs;
using Microsoft.AspNet.SignalR.Client;
using Shared.dto.Satellite;
using Shared.dto.Planet;
using Shared;
using System.Collections.Generic;

[assembly: OwinStartupAttribute(typeof(ClientWeb.Startup))]
namespace ClientWeb
{
    public partial class Startup
    {
        private string viewerName = string.Empty;
        public static IHubProxy _hub = null;
        private string url = string.Empty;
        private static Queue<Status> updates = null;

        public Startup()
        {
            url = DriverConstants.SIGNALR_URL;
            viewerName = "SatellitePlanetViewer";
            updates = new Queue<Status>();
        }

        public void Configuration(IAppBuilder app)
        {
            InitializeSignalR();
            _hub.On("UpdateViewerPosition", x => ProcessUpdates(x));
        }
        public static IList<Status> GetStatus()
        {
            IList<Status> statuses = new List<Status>();
            int curCount = updates.Count;

            if (updates != null && updates.Count > 0)
            {
                //TODO - pass back more than one update to the javascript
                while (curCount > 0)
                {
                    statuses.Add(updates.Dequeue());
                    break;
                }
            }

            return statuses;
        }

        private void ProcessUpdates(string status)
        {
            Status s = Utilities.DeserializeStatus(status);

            if (s != null)
                updates.Enqueue(s);
        }
        private void InitializeSignalR()
        {
            var connection = new HubConnection(url);
            _hub = connection.CreateHubProxy("PlanetServerHub");
            connection.Start().Wait();
        }
    }
}
