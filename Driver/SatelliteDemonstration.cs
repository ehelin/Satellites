using System.Threading;
using Shared;

namespace Driver
{
    public class SatelliteDemonstration
    {
        private string url = string.Empty;
        private int millisecondsBetweenClientCalls = 0;

        public SatelliteDemonstration(string pUrl, int pMillisecondsBetweenClientCalls)
        {
            url = pUrl;
            millisecondsBetweenClientCalls = pMillisecondsBetweenClientCalls;
            Data.Database db = new Data.Database();

            db.Truncate();
        }

        public void Run()
        {
            RunPlanet();

            System.Threading.Thread.Sleep(5000); //Wait for viewer to launch

            RunSatellites();
        }

        //https://msdn.microsoft.com/en-us/library/7a2f3ay4%28v=vs.90%29.aspx
        private void RunPlanet()
        {
            Planet.Planet p = new Planet.Planet(SatellitePositionConstants.ORIGIN_X, SatellitePositionConstants.ORIGIN_Y, url);
            Thread server = new Thread(p.DoWork);
            server.Start();
        }

        private void RunSatellites()
        {
            StartSatellite(SatellitePositionConstants.EAST_DESTINATION_X,
                            SatellitePositionConstants.EAST_DESTINATION_Y,
                            SatelliteConstants.EAST_SATELLITE,
                            SatelliteConstants.EAST_SATELLITE,
                            SatelliteFuelConstants.MAXIMUM,
                            SatellitePowerConstants.MAXIMUM);

            StartSatellite(SatellitePositionConstants.NORTHEAST_DESTINATION_X,
                            SatellitePositionConstants.NORTHEAST_DESTINATION_Y,
                            SatelliteConstants.NORTHEAST_SATELLITE,
                            SatelliteConstants.EAST_SATELLITE,
                            SatelliteFuelConstants.MAXIMUM,
                            SatellitePowerConstants.MAXIMUM);

            StartSatellite(SatellitePositionConstants.NORTH_DESTINATION_X,
                            SatellitePositionConstants.NORTH_DESTINATION_Y,
                            SatelliteConstants.NORTH_SATELLITE,
                            SatelliteConstants.NORTH_SATELLITE,
                            SatelliteFuelConstants.MAXIMUM,
                            SatellitePowerConstants.MAXIMUM);

            StartSatellite(SatellitePositionConstants.NORTHWEST_DESTINATION_X,
                            SatellitePositionConstants.NORTHWEST_DESTINATION_Y,
                            SatelliteConstants.NORTHWEST_SATELLITE,
                            SatelliteConstants.NORTH_SATELLITE,
                            SatelliteFuelConstants.MAXIMUM,
                            SatellitePowerConstants.MAXIMUM);

            StartSatellite(SatellitePositionConstants.WEST_DESTINATION_X,
                            SatellitePositionConstants.WEST_DESTINATION_Y,
                            SatelliteConstants.WEST_SATELLITE,
                            SatelliteConstants.WEST_SATELLITE,
                            SatelliteFuelConstants.MAXIMUM,
                            SatellitePowerConstants.MAXIMUM);

            StartSatellite(SatellitePositionConstants.SOUTHWEST_DESTINATION_X,
                            SatellitePositionConstants.SOUTHWEST_DESTINATION_Y,
                            SatelliteConstants.SOUTHWEST_SATELLITE,
                            SatelliteConstants.WEST_SATELLITE,
                            SatelliteFuelConstants.MAXIMUM,
                            SatellitePowerConstants.MAXIMUM);

            StartSatellite(SatellitePositionConstants.SOUTH_DESTINATION_X,
                            SatellitePositionConstants.SOUTH_DESTINATION_Y,
                            SatelliteConstants.SOUTH_SATELLITE,
                            SatelliteConstants.SOUTH_SATELLITE,
                            SatelliteFuelConstants.MAXIMUM,
                            SatellitePowerConstants.MAXIMUM);

            StartSatellite(SatellitePositionConstants.SOUTHEAST_DESTINATION_X,
                            SatellitePositionConstants.SOUTHEAST_DESTINATION_Y,
                            SatelliteConstants.SOUTHEAST_SATELLITE,
                            SatelliteConstants.SOUTH_SATELLITE,
                            SatelliteFuelConstants.MAXIMUM,
                            SatellitePowerConstants.MAXIMUM);
        }

        private void StartSatellite(int pDestX,
                                    int pDestY,
                                    string satName,
                                    string ascent,
                                    decimal fuel,
                                    decimal power)
        {
            Satellite.Satellite s = new Satellite.Satellite(SatellitePositionConstants.INITIAL_X,
                                                            SatellitePositionConstants.INITIAL_Y,
                                                            SatellitePositionConstants.ORIGIN_X,
                                                            SatellitePositionConstants.ORIGIN_Y,
                                                            pDestX,
                                                            pDestY,
                                                            url,
                                                            satName,
                                                            millisecondsBetweenClientCalls,
                                                            false,
                                                            ascent,
                                                            fuel,
                                                            power);
            Thread client = new Thread(s.DoWork);
            client.Start();
        }
    }
}
