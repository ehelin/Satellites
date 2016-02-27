using System;
using System.Collections.Generic;
using Shared.SignalR;
using Shared.dto.Satellite;
using Shared.dto.Planet;
using Shared;
using System.Threading;

namespace Planet
{
    [Microsoft.AspNet.SignalR.Hubs.HubName("PlanetServerHub")]
    public class PlanetServerHub : ServerHub
    {
        public static Queue<Status> statusesToDump = null;
        public static Queue<SatelliteClient> clientsToDump = null;
        public static List<SatelliteClient> satellites = null;
        private static NetworkShiftThread shiftThread = null;
        private static ArchiveUpdates updateDumper = null;

        public PlanetServerHub()
        {
            if (satellites == null)
                satellites = new List<SatelliteClient>();

            if (statusesToDump == null)
                statusesToDump = new Queue<Status>();

            if (clientsToDump == null)
                clientsToDump = new Queue<SatelliteClient>();

            if (shiftThread == null)
            {
                shiftThread = NetworkShiftThread.Instance;
                NetworkShiftThread.SetPlanetServerHubInstance(this);
                Thread t = new Thread(shiftThread.DoWork);
                t.Start();
            }

            if (updateDumper == null)
            {
                updateDumper = ArchiveUpdates.Instance;
                ArchiveUpdates.SetPlanetServerHubInstance(this);
                Thread t = new Thread(updateDumper.DoWork);
                t.Start();
            }
        }

        public void RegisterSatellite(string status)
        {
            Status s = Utilities.DeserializeStatus(status);

            if (!satellites.Exists(x => x.SatelliteName.Equals(s.SatelliteName)))
            {
                SatelliteClient sc = new SatelliteClient(s.SatelliteName, s.onStation, s.solarPanelsDeployed);

                clientsToDump.Enqueue(sc);
                satellites.Add(sc);
                NetworkShiftThread.AddSatellite(sc);
            }
        }

        public void UpdateClients(Status s)
        {
            SatelliteClient sc = satellites.Find(x => x.SatelliteName.Equals(s.SatelliteName));

            if (sc != null)
            {
                sc.SatelliteName = s.SatelliteName;
                sc.Onstation = s.onStation;
                sc.SolarPanelsDeployed = s.solarPanelsDeployed;
                sc.PlanetShift = s.PlanetShift;
                sc.DestinationX = s.DestinationX;
                sc.DestinationY = s.DestinationY;

                shiftThread.UpdateSatellite(sc);
            }
        }

        public void SendNetworkShift(SatelliteClient sc)
        {
            string shiftResult = Utilities.SerializeSatelliteClient(sc);
            clientsToDump.Enqueue(sc);
            Clients.All.ProcessUpdate(shiftResult);
        }

        public void SendPosition(string status)
        {
            Status s = Utilities.DeserializeStatus(status);
            UpdateClients(s);

            statusesToDump.Enqueue(s);
            Clients.All.UpdateViewerPosition(status);

            Console.WriteLine(s.ToString());
        }
    }
}
