using System;
using Shared;
using Shared.dto.Planet;
using Shared.dto.Satellite;
using Shared.threading;

namespace Planet
{
    public class ArchiveUpdates : ThreadBase
    {
        private bool processing = false;
        private static Data.Database db = new Data.Database();
        private static ArchiveUpdates instance;
        private static PlanetServerHub psh = null;

        public static void SetPlanetServerHubInstance(PlanetServerHub pPsh)
        {
            psh = pPsh;
        }

        public static ArchiveUpdates Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ArchiveUpdates();
                }

                return instance;
            }
        }

        public override void DoWork()
        {
            while (!this._shouldStop)
            {
                if (!processing)
                {
                    int clientCnt = PlanetServerHub.clientsToDump.Count;
                    int updateCnt = PlanetServerHub.statusesToDump.Count;

                    if (clientCnt > 0 || updateCnt > 0)
                    {
                        processing = true;

                        ArchiveClientData(clientCnt);
                        ArchiveStatusData(updateCnt);

                        processing = false;
                    }
                }

                System.Threading.Thread.Sleep(Constants.DEFAULT_SLEEP_TIME);
            }
        }

        private void ArchiveClientData(int count)
        {
            int ctr = 0;                                                            //hack

            while (ctr < count)
            {
                if (PlanetServerHub.clientsToDump.Count < 1)                        //hack
                    break;

                SatelliteClient sc = PlanetServerHub.clientsToDump.Dequeue();
                string status = Utilities.SerializeSatelliteClient(sc);
                ArchiveClientUpdate(sc, status);
                ctr++;                                                              //hack
            }
        }
        private void ArchiveStatusData(int count)
        {
            int ctr = 0;                                                            //hack

            while (ctr < count)
            {
                if (PlanetServerHub.statusesToDump.Count < 1)                       //hack
                    break;

                Status s = PlanetServerHub.statusesToDump.Dequeue();
                string status = Utilities.SerializeStatus(s);
                ArchiveStatusUpdate(s, status);
                ctr++;                                                              //hack
            }
        }
        private void ArchiveClientUpdate(SatelliteClient sc, string data)
        {
            if (sc != null && !string.IsNullOrEmpty(data))
            {
                string type = sc.SatelliteName + Enumerations.UpdateType._ClientUpdate_.ToString() + DateTime.Now.ToString(PlanetConstants.ARCHIVE_UPDATE_DATE_FORMAT);
                db.InsertUpdate(type, data);
            }
        }
        private void ArchiveStatusUpdate(Status s, string data)
        {
            if (s != null && !string.IsNullOrEmpty(data))
            {
                string type = s.SatelliteName + Enumerations.UpdateType._StatusUpdate_.ToString() + DateTime.Now.ToString(PlanetConstants.ARCHIVE_UPDATE_DATE_FORMAT);
                db.InsertUpdate(type, data);
            }
        }
    }
}
