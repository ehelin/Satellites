namespace Shared.dto.Planet
{
    public class SatelliteClient
    {
        public string SatelliteName { get; set; }
        public bool Onstation { get; set; }
        public bool SolarPanelsDeployed { get; set; }
        public bool PlanetShift { get; set; }
        public int DestinationX { get; set; }
        public int DestinationY { get; set; }

        public SatelliteClient(string pSatelliteName, bool pOnstation, bool pSolarPanelsDeployed)
        {
            SatelliteName = pSatelliteName;
            Onstation = pOnstation;
            SolarPanelsDeployed = pSolarPanelsDeployed;
        }
    }
}
