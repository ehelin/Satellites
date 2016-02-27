using System;
using Microsoft.AspNet.SignalR.Client;
using System.Threading;
using Shared;
using Shared.dto.Satellite;
using Shared.dto.Planet;
using Shared.threading;

namespace Satellite
{
    //NOTE:  For purposes of this demonstration, the navigation is a 2 dimensional X/Y coordinate system where the distance from point to point is one mile and the 
    //       center of this universion is the 2 dimensional center of the Earth.
    public class Satellite : ThreadBase
    {
        //add gravity as a force that navigation has to deal with
        //add a factor to have each satellite handle fuel/power differently (small difference)
        //add additional onboard systems requireming power
        private string driftDirection = string.Empty;
        private int driftCounter = 0;
        private Status SatelliteStatus = null;
        private IHubProxy _hub = null;
        private string url = string.Empty;
        private int sleepTime = 0;
        private bool clientWriteMsgs = false;
        private bool runDrift = false;

        public Satellite(int pX,
                         int pY,
                         int pOriginX,
                         int pOriginY,
                         int pDestinationX,
                         int pDestinationY,
                         string pUrl,
                         string pName,
                         int pSleepTime,
                         bool pClientWritesMsgs,
                         string pAscentDirection,
                         decimal pFuel,
                         decimal pPower)
        {
            SatelliteStatus = new Status(pX,
                                         pY,
                                         pOriginX,
                                         pOriginY,
                                         pDestinationX,
                                         pDestinationY,
                                         pName,
                                         pAscentDirection,
                                         false,
                                         false,
                                         pFuel,
                                         pPower);

            driftDirection = pAscentDirection;
            url = pUrl;
            sleepTime = pSleepTime;
            clientWriteMsgs = pClientWritesMsgs;
        }

        public override void DoWork()
        {
            _hub = StartClient();

            _hub.On("ProcessUpdate", x => ProcessUpdate(x));
            string status = Utilities.SerializeStatus(this.SatelliteStatus);
            _hub.Invoke("RegisterSatellite", status);

            ReportPosition();

            while (!_shouldStop)
            {
                bool positionMoved = RunNavigation();
                RunPower(positionMoved);
                ConditionCheck(positionMoved);

                ReportPosition();

                Thread.Sleep(sleepTime);
            }

            Console.WriteLine(SatelliteStatus.SatelliteName + " stopping...");
        }
        
        private void ProcessUpdate(string result)
        {
            SatelliteClient sc = Utilities.DeserializeSatelliteClient(result);

            if (sc.SatelliteName.Equals(this.SatelliteStatus.SatelliteName))
            {
                this.SatelliteStatus.onStation = sc.Onstation;
                this.SatelliteStatus.solarPanelsDeployed = sc.SolarPanelsDeployed;
                this.SatelliteStatus.PlanetShift = sc.PlanetShift;
                this.SatelliteStatus.DestinationX = sc.DestinationX;
                this.SatelliteStatus.DestinationY = sc.DestinationY;
            }
        }
        private void ConditionCheck(bool positionMoved)
        {
            if (positionMoved)
                SatelliteStatus.fuel = SatelliteStatus.fuel - SatelliteFuelConstants.FUEL_COST_PER_POINT;

            if (SatelliteStatus.SatellitePosition.X == SatelliteStatus.DestinationX && SatelliteStatus.SatellitePosition.Y == SatelliteStatus.DestinationY && !SatelliteStatus.onStation)
            {
                SatelliteStatus.onStation = true;
                SatelliteStatus.PlanetShift = false;
            }

            if (SatelliteStatus.SatellitePosition.X != SatelliteStatus.DestinationX || SatelliteStatus.SatellitePosition.Y != SatelliteStatus.DestinationY || !SatelliteStatus.onStation)
                SatelliteStatus.onStation = false;

            if (SatelliteStatus.onStation)
            {
                runDrift = true;

                if (!SatelliteStatus.solarPanelsDeployed)
                    SatelliteStatus.solarPanelsDeployed = true;
            }
            else
            {
                runDrift = false;
                SatelliteStatus.solarPanelsDeployed = false;
            }
        }
        private void RunPower(bool positionMoved)
        {
            if (positionMoved)
                SatelliteStatus.power = SatelliteStatus.power - SatellitePowerConstants.POWER_COST_NAVIGATION_PER_POINT;

            if (SatelliteStatus.solarPanelsDeployed && SatelliteStatus.power < SatellitePowerConstants.MAXIMUM)
            {
                SatelliteStatus.power = SatelliteStatus.power + SatellitePowerConstants.POWER_GAIN_NAVIGATION_PER_INTERATION;
            }

            //TODO - make this more believable :)
            if (SatelliteStatus.solarPanelsDeployed && SatelliteStatus.fuel < SatelliteFuelConstants.MAXIMUM)
            {
                SatelliteStatus.fuel = SatelliteStatus.fuel + SatellitePowerConstants.POWER_GAIN_NAVIGATION_PER_INTERATION;
            }
        }
        private bool RunNavigation()
        {
            bool positionMoved = false;

            if (SatelliteStatus.onStation && runDrift)
                RunDrift();

            positionMoved = MoveSatellite();

            return positionMoved;
        }
        private void RunDrift()
        {
            if (driftCounter > SatellitePositionConstants.DRIFT_COUNTER_MAX)
            {
                int aser = 1;
            }
            if (driftCounter > SatellitePositionConstants.DRIFT_COUNTER_MAX && driftDirection == SatelliteConstants.NORTH_SATELLITE)
            {
                this.SatelliteStatus.onStation = false;
                this.SatelliteStatus.solarPanelsDeployed = false;
                SatelliteStatus.SatellitePosition.X = SatelliteStatus.SatellitePosition.X + SatellitePositionConstants.DRIFT_ADD_FACTOR;
                driftDirection = SatelliteConstants.EAST_SATELLITE;
                driftCounter = SatellitePositionConstants.DRIFT_COUNTER_MIN;
            }
            else if (driftCounter > SatellitePositionConstants.DRIFT_COUNTER_MAX && driftDirection == SatelliteConstants.EAST_SATELLITE)
            {
                this.SatelliteStatus.onStation = false;
                this.SatelliteStatus.solarPanelsDeployed = false;
                SatelliteStatus.SatellitePosition.Y = SatelliteStatus.SatellitePosition.Y + SatellitePositionConstants.DRIFT_ADD_FACTOR;
                driftDirection = SatelliteConstants.SOUTH_SATELLITE;
                driftCounter = SatellitePositionConstants.DRIFT_COUNTER_MIN;
            }
            else if (driftCounter > SatellitePositionConstants.DRIFT_COUNTER_MAX && driftDirection == SatelliteConstants.SOUTH_SATELLITE)
            {
                this.SatelliteStatus.onStation = false;
                this.SatelliteStatus.solarPanelsDeployed = false;
                SatelliteStatus.SatellitePosition.X = SatelliteStatus.SatellitePosition.X - SatellitePositionConstants.DRIFT_ADD_FACTOR;
                driftDirection = SatelliteConstants.WEST_SATELLITE;
                driftCounter = SatellitePositionConstants.DRIFT_COUNTER_MIN;
            }
            else if (driftCounter > SatellitePositionConstants.DRIFT_COUNTER_MAX && driftDirection == SatelliteConstants.WEST_SATELLITE)
            {
                this.SatelliteStatus.onStation = false;
                this.SatelliteStatus.solarPanelsDeployed = false;
                SatelliteStatus.SatellitePosition.Y = SatelliteStatus.SatellitePosition.Y - SatellitePositionConstants.DRIFT_ADD_FACTOR;
                driftDirection = SatelliteConstants.NORTH_SATELLITE;
                driftCounter = SatellitePositionConstants.DRIFT_COUNTER_MIN;
            }

            driftCounter++;
        }
        private bool MoveSatellite()
        {
            bool positionMoved = false;

            if (CanMove() && (SatelliteStatus.SatellitePosition.X != SatelliteStatus.DestinationX || SatelliteStatus.SatellitePosition.Y != SatelliteStatus.DestinationY))
            {
                if (SatelliteStatus.SatellitePosition.X != SatelliteStatus.DestinationX && SatelliteStatus.SatellitePosition.X < SatelliteStatus.DestinationX)
                {
                    SatelliteStatus.SatellitePosition.X++;
                    positionMoved = true;
                }

                if (SatelliteStatus.SatellitePosition.X != SatelliteStatus.DestinationX && SatelliteStatus.SatellitePosition.X > SatelliteStatus.DestinationX)
                {
                    SatelliteStatus.SatellitePosition.X--;
                    positionMoved = true;
                }

                if (SatelliteStatus.SatellitePosition.Y != SatelliteStatus.DestinationY && SatelliteStatus.SatellitePosition.Y < SatelliteStatus.DestinationY)
                {
                    SatelliteStatus.SatellitePosition.Y++;
                    positionMoved = true;
                }

                if (SatelliteStatus.SatellitePosition.Y != SatelliteStatus.DestinationY && SatelliteStatus.SatellitePosition.Y > SatelliteStatus.DestinationY)
                {
                    SatelliteStatus.SatellitePosition.Y--;
                    positionMoved = true;
                }
            }

            return positionMoved;
        }
        private bool CanMove()
        {
            bool canMove = false;

            if (SatelliteStatus.fuel >= SatelliteFuelConstants.FUEL_COST_PER_POINT)
                canMove = true;

            return canMove;
        }
        private void ReportPosition()
        {
            string status = Utilities.SerializeStatus(this.SatelliteStatus);
            _hub.Invoke("SendPosition", status).Wait();    //TODO - do we want to wait here?  Seems like no...
        }
        private IHubProxy StartClient()
        {
            IHubProxy _hub;

            try
            {
                var connection = new HubConnection(url);
                _hub = connection.CreateHubProxy("PlanetServerHub");
                connection.Start().Wait();
            }
            catch (Exception e)
            {
                throw e;
            }

            return _hub;
        }
    }
}
