using Shared.dto.Satellite;
using Shared.dto.Planet;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace Shared
{
    public class Utilities
    {
        public static string SerializeStatus(Status s)
        {
            string serializedValue = string.Empty;

            serializedValue = JsonConvert.SerializeObject(s);

            return serializedValue;
        }
        public static Status DeserializeStatus(string serializedValue)
        {
            Status s = null;

            s = JsonConvert.DeserializeObject<Status>(serializedValue);

            return s;
        }
        public static string SerializeSatelliteClient(SatelliteClient sc)
        {
            string serializedValue = string.Empty;

            serializedValue = JsonConvert.SerializeObject(sc);

            return serializedValue;
        }
        public static SatelliteClient DeserializeSatelliteClient(string serializedValue)
        {
            SatelliteClient sc = null;

            sc = JsonConvert.DeserializeObject<SatelliteClient>(serializedValue);

            return sc;
        }
        public static void CloseDbObjects(SqlConnection conn,
                                         SqlCommand cmd,
                                         SqlDataReader rdr,
                                         SqlDataAdapter da)
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }

            if (rdr != null)
            {
                rdr.Close();
                rdr.Dispose();
                rdr = null;
            }

            if (da != null)
            {
                da.Dispose();
                da = null;
            }
        }
    }
}
