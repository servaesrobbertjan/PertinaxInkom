using MySqlConnector;
using PertinaxInkom.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization.DataContracts;
using System.Text;
using System.Threading.Tasks;

namespace PertinaxInkom
{
    public class clsAddressDB
    {
        ADOConnection A = new ADOConnection();

        public clsAddress getAddress(int id)
        {
            MySqlConnection CN = new MySqlConnection(A.Pertinaxlanstr);
            MySqlCommand CMD = new MySqlCommand(Properties.StoredProcedures.S_Address, CN);
            CMD.CommandType = CommandType.StoredProcedure;
            CMD.Parameters.AddWithValue("p_id", id);
            
            try
            {
                CN.Open();
                MySqlDataReader reader = CMD.ExecuteReader();

                if (reader.Read())
                {
                    clsAddress address = new clsAddress(
                        Convert.ToInt32(reader["id"]),
                        Convert.ToString(reader["street_name"]),
                        Convert.ToString(reader["house_number"]),
                        Convert.ToString(reader["bus_number"]),
                        Convert.ToString(reader["zip_code"]),
                        Convert.ToString(reader["city"]),
                        Convert.ToString(reader["country"]),
                        Convert.ToDateTime(reader["timestamp"]));

                    return address;
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

        }

        public int? GetAddresByFullAddress(string streetname, string housenumber, string busnumber, string zipcode, string city, string country)
        {
            MySqlConnection CN = new MySqlConnection(A.Pertinaxlanstr);
            MySqlCommand CMD = new MySqlCommand(Properties.StoredProcedures.S_AddressByFullAddress, CN);
            CMD.CommandType = CommandType.StoredProcedure;
            CMD.Parameters.AddWithValue("p_street_name", streetname);
            CMD.Parameters.AddWithValue("p_house_number", housenumber);
            if (busnumber != null)
            {
                CMD.Parameters.AddWithValue("p_bus_number", busnumber);
            }
            else
            {
                CMD.Parameters.Add("p_bus_number", MySqlDbType.VarChar).Value = DBNull.Value;
            }
            CMD.Parameters.AddWithValue("p_zip_code", zipcode);
            CMD.Parameters.AddWithValue("p_city", city);
            CMD.Parameters.AddWithValue("p_country", country);

            try
            {
                CN.Open();
                MySqlDataReader reader = CMD.ExecuteReader(CommandBehavior.SingleResult);

                if (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    return id;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CN.Close();
            }
        }

        public int CreateAddress(string streetname, string housenumber, string busnumber, string zipcode, string city, string country)
        {
            try
            {
                MySqlConnection CN = new MySqlConnection(A.Pertinaxlanstr);
                MySqlCommand CMD = new MySqlCommand(Properties.StoredProcedures.C_Address, CN);
                CMD.CommandType = CommandType.StoredProcedure;
                CMD.Parameters.AddWithValue("p_street_name", streetname);
                CMD.Parameters.AddWithValue("p_house_number", housenumber);
                if (busnumber == null)
                {
                    CMD.Parameters.AddWithValue("p_bus_number", DBNull.Value);
                }
                else
                {
                    CMD.Parameters.AddWithValue("p_bus_number", busnumber);
                }
                CMD.Parameters.AddWithValue("p_zip_code", zipcode);
                CMD.Parameters.AddWithValue("p_city", city);
                CMD.Parameters.AddWithValue("p_country", country);

                CN.Open();
                int id = Convert.ToInt32(CMD.ExecuteScalar());
                CN.Close();

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public bool UpdateAddress(int id, string streetname, string housenumber, string busnumber, string zipcode, string city, string country)
        {
            try
            {
                MySqlConnection CN = new MySqlConnection(A.Pertinaxlanstr);
                MySqlCommand CMD = new MySqlCommand(Properties.StoredProcedures.U_Address, CN);
                CMD.CommandType = CommandType.StoredProcedure;
                CMD.Parameters.AddWithValue("p_id", id);
                CMD.Parameters.AddWithValue("p_street_name", streetname);
                CMD.Parameters.AddWithValue("p_house_number", housenumber);
                if (busnumber == null)
                {
                    CMD.Parameters.AddWithValue("p_bus_number", DBNull.Value);
                }
                else
                {
                    CMD.Parameters.AddWithValue("p_bus_number", busnumber);
                }
                CMD.Parameters.AddWithValue("p_zip_code", zipcode);
                CMD.Parameters.AddWithValue("p_city", city);
                CMD.Parameters.AddWithValue("p_country", country);

                CN.Open();
                CMD.ExecuteNonQuery();
                CN.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
