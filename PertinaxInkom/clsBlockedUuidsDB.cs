using MySqlConnector;
using PertinaxInkom.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PertinaxInkom
{
    public class clsBlockedUuidsDB
    {
        ADOConnection BU = new ADOConnection();

        public clsBlockedUuids GetBlockedUidByUuid(string uuid)
        {
            MySqlConnection CN = new MySqlConnection(BU.Pertinaxlanstr);
            MySqlCommand CMD = new MySqlCommand("S_BlockedUuidByUuid", CN);
            CMD.CommandType = CommandType.StoredProcedure;
            CMD.Parameters.AddWithValue("P_uuid", uuid);

            try
            {
                CN.Open();
                MySqlDataReader reader = CMD.ExecuteReader();

                if (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);

                    clsBlockedUuids blockedUuid = new clsBlockedUuids(
                        id,
                        uuid);

                    return blockedUuid;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CN.Close();
            }
        }

        public int? CreateBlockedUuid(string uuid)
        {
            try
            {
                MySqlConnection CN = new MySqlConnection(BU.Pertinaxlanstr);
                MySqlCommand CMD = new MySqlCommand("C_BlockedUuid", CN);
                CMD.CommandType = CommandType.StoredProcedure;
                CMD.Parameters.AddWithValue("p_uuid", uuid);

                CN.Open();
                object result = CMD.ExecuteScalar();
                CN.Close();

                return result != null ? Convert.ToInt32(result) : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
