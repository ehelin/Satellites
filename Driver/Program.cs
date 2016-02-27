using Shared;

namespace Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            RunSatellites();
        }

        private static void RunSatellites()
        {
            SatelliteDemonstration sd = new SatelliteDemonstration(DriverConstants.SIGNALR_URL, DriverConstants.APPLICATION_INTERVAL);
            sd.Run();
        }
    }
}
