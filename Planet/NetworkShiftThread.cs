using System;
using System.Collections.Generic;
using Shared;
using Shared.dto.Planet;
using Shared.threading;

namespace Planet
{
    public class NetworkShiftThread : ThreadBase
    {
        private DateTime StartRunTme = DateTime.Now;
        private DateTime LastNetworkShiftTime = DateTime.MinValue;
        private bool shiftReverse = false;
        private static List<SatelliteClient> satellites = new List<SatelliteClient>();
        private static NetworkShiftThread instance;
        private static PlanetServerHub psh = null;

        public static void SetPlanetServerHubInstance(PlanetServerHub pPsh)
        {
            psh = pPsh;
        }
        public static NetworkShiftThread Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NetworkShiftThread();
                }

                return instance;
            }
        }

        public static void AddSatellite(SatelliteClient sc)
        {
            if (!satellites.Contains(sc))
                satellites.Add(sc);
        }

        public void UpdateSatellite(SatelliteClient sc)
        {
            SatelliteClient curSc = satellites.Find(x => x.SatelliteName.Equals(sc.SatelliteName));

            if (curSc != null)
            {
                curSc.SatelliteName = sc.SatelliteName;
                curSc.Onstation = sc.Onstation;
                curSc.SolarPanelsDeployed = sc.SolarPanelsDeployed;
                curSc.PlanetShift = sc.PlanetShift;
                curSc.DestinationX = sc.DestinationX;
                curSc.DestinationY = sc.DestinationY;
            }
        }

        public override void DoWork()
        {
            while (!this._shouldStop)
            {
                InitiateNetworkShift();
                System.Threading.Thread.Sleep(PlanetConstants.NETWORK_SHIFT_SLEEP_INTERVAL);
            }
        }

        private void InitiateNetworkShift()
        {
            if (CanShiftNetwork())
            {
                if (shiftReverse)
                    shiftReverse = false;
                else
                    shiftReverse = true;

                ExecuteNetworkShift();

                LastNetworkShiftTime = DateTime.Now.AddMinutes(PlanetConstants.NETWORK_SHIFT_MINUTE_INTERVAL);
            }
        }
        private void ExecuteNetworkShift()
        {
            foreach (SatelliteClient sc in satellites)
            {
                sc.PlanetShift = true;
                sc.Onstation = false;
                sc.SolarPanelsDeployed = false;

                if (sc.SatelliteName.Equals(SatelliteConstants.WEST_SATELLITE))
                {
                    if (shiftReverse)
                        sc.DestinationY = sc.DestinationY - PlanetConstants.NETWORK_SHIFT_FACTOR;
                    else
                        sc.DestinationY = sc.DestinationY + PlanetConstants.NETWORK_SHIFT_FACTOR;
                }

                else if (sc.SatelliteName.Equals(SatelliteConstants.NORTHWEST_SATELLITE))
                {
                    if (shiftReverse)
                    {
                        sc.DestinationX = sc.DestinationX + PlanetConstants.NETWORK_SHIFT_FACTOR;
                        sc.DestinationY = sc.DestinationY - PlanetConstants.NETWORK_SHIFT_FACTOR;
                    }
                    else
                    {
                        sc.DestinationX = sc.DestinationX - PlanetConstants.NETWORK_SHIFT_FACTOR;
                        sc.DestinationY = sc.DestinationY + PlanetConstants.NETWORK_SHIFT_FACTOR;
                    }
                }

                else if (sc.SatelliteName.Equals(SatelliteConstants.NORTH_SATELLITE))
                {
                    if (shiftReverse)
                        sc.DestinationX = sc.DestinationX + PlanetConstants.NETWORK_SHIFT_FACTOR;
                    else
                        sc.DestinationX = sc.DestinationX - PlanetConstants.NETWORK_SHIFT_FACTOR;
                }

                else if (sc.SatelliteName.Equals(SatelliteConstants.NORTHEAST_SATELLITE))
                {
                    if (shiftReverse)
                    {
                        sc.DestinationX = sc.DestinationX + PlanetConstants.NETWORK_SHIFT_FACTOR;
                        sc.DestinationY = sc.DestinationY + PlanetConstants.NETWORK_SHIFT_FACTOR;
                    }
                    else
                    {
                        sc.DestinationX = sc.DestinationX - PlanetConstants.NETWORK_SHIFT_FACTOR;
                        sc.DestinationY = sc.DestinationY - PlanetConstants.NETWORK_SHIFT_FACTOR;
                    }
                }

                else if (sc.SatelliteName.Equals(SatelliteConstants.EAST_SATELLITE))
                {
                    if (shiftReverse)
                        sc.DestinationY = sc.DestinationY + PlanetConstants.NETWORK_SHIFT_FACTOR;
                    else
                        sc.DestinationY = sc.DestinationY - PlanetConstants.NETWORK_SHIFT_FACTOR;
                }

                else if (sc.SatelliteName.Equals(SatelliteConstants.SOUTHEAST_SATELLITE))
                {
                    if (shiftReverse)
                    {
                        sc.DestinationX = sc.DestinationX - PlanetConstants.NETWORK_SHIFT_FACTOR;
                        sc.DestinationY = sc.DestinationY + PlanetConstants.NETWORK_SHIFT_FACTOR;
                    }
                    else
                    {
                        sc.DestinationX = sc.DestinationX + PlanetConstants.NETWORK_SHIFT_FACTOR;
                        sc.DestinationY = sc.DestinationY - PlanetConstants.NETWORK_SHIFT_FACTOR;
                    }
                }

                else if (sc.SatelliteName.Equals(SatelliteConstants.SOUTH_SATELLITE))
                {
                    if (shiftReverse)
                        sc.DestinationX = sc.DestinationX - PlanetConstants.NETWORK_SHIFT_FACTOR;
                    else
                        sc.DestinationX = sc.DestinationX + PlanetConstants.NETWORK_SHIFT_FACTOR;
                }

                else if (sc.SatelliteName.Equals(SatelliteConstants.SOUTHWEST_SATELLITE))
                {
                    if (shiftReverse)
                    {
                        sc.DestinationX = sc.DestinationX - PlanetConstants.NETWORK_SHIFT_FACTOR;
                        sc.DestinationY = sc.DestinationY - PlanetConstants.NETWORK_SHIFT_FACTOR;
                    }
                    else
                    {
                        sc.DestinationX = sc.DestinationX + PlanetConstants.NETWORK_SHIFT_FACTOR;
                        sc.DestinationY = sc.DestinationY + PlanetConstants.NETWORK_SHIFT_FACTOR;
                    }
                }

                else
                    throw new Exception(ExceptionConstants.ERR_0001);

                psh.SendNetworkShift(sc);
            }
        }
        private bool CanShiftNetwork()
        {
            bool canShift = false;
            TimeSpan timespan = (DateTime.Now - StartRunTme);

            //initial
            if (LastNetworkShiftTime == DateTime.MinValue
                    && timespan.Minutes >= PlanetConstants.NETWORK_SHIFT_MINUTE_INTERVAL
                        && satellites.Count > 0)
                canShift = true;

            //recurring
            else if (LastNetworkShiftTime != DateTime.MinValue
                    && LastNetworkShiftTime < DateTime.Now)
                canShift = true;

            if (canShift && !AllSatellitesOnstation())
                canShift = false;

            return canShift;
        }
        private bool AllSatellitesOnstation()
        {
            bool onstation = true;

            foreach (SatelliteClient sc in satellites)
            {
                if (!sc.Onstation || !sc.SolarPanelsDeployed)
                {
                    onstation = false;
                    break;
                }
            }

            return onstation;
        }
    }
}


