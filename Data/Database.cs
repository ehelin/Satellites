using System;
using System.Data.SqlClient;
using Shared;

namespace Data
{
    public class Database
    {
        public void InsertUpdate(string type, string data)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            string curFile = string.Empty;

            try
            {
                conn = new SqlConnection(DatabaseConstants.DB_CONNECTION);
                cmd = new SqlCommand();
                cmd.CommandTimeout = DatabaseConstants.DB_TIMEOUT;
                cmd.Connection = conn;
                cmd.CommandText = DatabaseConstants.SQL_INSERT_UPDATE;
                cmd.CommandType = System.Data.CommandType.Text;
                
                cmd.Parameters.Add(new SqlParameter(DatabaseConstants.SQL_INSERT_UPDATE_VAR_TYPE, type));
                cmd.Parameters.Add(new SqlParameter(DatabaseConstants.SQL_INSERT_UPDATE_VAR_DATA, data));
                cmd.Parameters.Add(new SqlParameter(DatabaseConstants.SQL_INSERT_UPDATE_VAR_CREATED, DateTime.Now));

                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Utilities.CloseDbObjects(conn, cmd, null, null);
            }
        }

        public void Truncate()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            string curFile = string.Empty;

            try
            {
                conn = new SqlConnection(DatabaseConstants.DB_CONNECTION);
                cmd = new SqlCommand();
                cmd.CommandTimeout = DatabaseConstants.DB_TIMEOUT;
                cmd.Connection = conn;
                cmd.CommandText = DatabaseConstants.SQL_TRUNCATE_UPDATE_TABLE;
                cmd.CommandType = System.Data.CommandType.Text;

                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Utilities.CloseDbObjects(conn, cmd, null, null);
            }
        }
    }
}
