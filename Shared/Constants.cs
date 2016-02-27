namespace Shared
{
    public class Constants
    {
        public const int DEFAULT_SLEEP_TIME = 1000;
    }
    public class DriverConstants
    {
        public const string SIGNALR_URL = "http://localhost:12345/";
        public const int APPLICATION_INTERVAL = 100;
    }
    public class SatelliteConstants
    {
        public const string WEST_SATELLITE = "West";
        public const string SOUTHWEST_SATELLITE = "SouthWest";
        public const string SOUTH_SATELLITE = "South";
        public const string SOUTHEAST_SATELLITE = "SouthEast";
        public const string EAST_SATELLITE = "East";
        public const string NORTHEAST_SATELLITE = "NorthEast";
        public const string NORTH_SATELLITE = "North";
        public const string NORTHWEST_SATELLITE = "NorthWest";
    }
    public class ClientWebConstants
    {
        public const string NO_READINGS = "No Updates!";
    }
    public class DatabaseConstants
    {
        public const int DB_TIMEOUT = 3000000;  //seconds

        public const string DB_CONNECTION = "Data Source=;Initial Catalog=Satellite;Integrated Security=true;";  

        public const string SQL_TRUNCATE_UPDATE_TABLE = "truncate table dbo.updates";
        public const string SQL_INSERT_UPDATE = "INSERT INTO [dbo].[Updates]  "
                                                + " ([type],  "
                                                + "  [data],  "
                                                + "  [created])  "
                                            + " VALUES  "
                                                + " (@type,  "
                                                + "  @data, "
                                                + "  @created)";
        public const string SQL_INSERT_UPDATE_VAR_TYPE = "@type";
        public const string SQL_INSERT_UPDATE_VAR_DATA = "@data";
        public const string SQL_INSERT_UPDATE_VAR_CREATED = "@created";
    }
    public class PlanetConstants
    {
        public const int NETWORK_SHIFT_FACTOR = 100;
        public const int NETWORK_SHIFT_MINUTE_INTERVAL = 2;
        public const int NETWORK_SHIFT_SLEEP_INTERVAL = 1000;      //seconds

        public const string ARCHIVE_UPDATE_DATE_FORMAT = "yyyymmddhhmmssffftt";
    }
    public class SatelliteFuelConstants
    {
        public const decimal MINIMUM = 0;
        public const decimal MAXIMUM = 100;
        public const decimal FUEL_COST_PER_POINT = (decimal).20;
        public const decimal FUEL_GAIN_NAVIGATION_PER_INTERATION = (decimal).1;
    }
    public class SatellitePowerConstants
    {
        public const decimal MINIMUM = 0;
        public const decimal MAXIMUM = 100;
        public const decimal POWER_COST_NAVIGATION_PER_POINT = (decimal).25;
        public const decimal POWER_GAIN_NAVIGATION_PER_INTERATION = (decimal).5;
    }
    public class SatellitePositionConstants
    {
        public const int INITIAL_X = 435;
        public const int INITIAL_Y = 380;

        public const int ORIGIN_X = 435;
        public const int ORIGIN_Y = 380;

        public const int NORTHEAST_DESTINATION_X = 563;
        public const int NORTHEAST_DESTINATION_Y = 265;

        public const int NORTH_DESTINATION_X = 435;
        public const int NORTH_DESTINATION_Y = 195;

        public const int NORTHWEST_DESTINATION_X = 290;
        public const int NORTHWEST_DESTINATION_Y = 265;

        public const int WEST_DESTINATION_X = 230;
        public const int WEST_DESTINATION_Y = 380;

        public const int SOUTHWEST_DESTINATION_X = 290;
        public const int SOUTHWEST_DESTINATION_Y = 495;

        public const int SOUTH_DESTINATION_X = 435;
        public const int SOUTH_DESTINATION_Y = 570;

        public const int SOUTHEAST_DESTINATION_X = 580;
        public const int SOUTHEAST_DESTINATION_Y = 495;

        public const int EAST_DESTINATION_X = 625;
        public const int EAST_DESTINATION_Y = 380;

        //drift is a random applied value applied to simulate satellite drift
        public const int DRIFT_ADD_FACTOR = 5;
        public const int DRIFT_COUNTER_MIN = 0;
        public const int DRIFT_COUNTER_MAX = 40;
    }
    public class ExceptionConstants
    {
        public const string ERR_0001 = "Unknown satellite name while attempting planet satellite network shift";
    }
}
