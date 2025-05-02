using MySqlConnector;
using PertinaxInkom.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PertinaxInkom
{
    public class clsTicketTypeDB
    {
        ADOConnection TT = new ADOConnection();

        public clsTicketType GetTicketType(string ticketname)
        {
            MySqlConnection CN = new MySqlConnection(TT.Pertinaxlanstr);
            MySqlCommand CMD = new MySqlCommand(Properties.StoredProcedures.S_TicketType, CN);
            CMD.CommandType = CommandType.StoredProcedure;
            CMD.Parameters.AddWithValue("p_name", ticketname);

            try
            {
                CN.Open();
                MySqlDataReader reader = CMD.ExecuteReader(CommandBehavior.SingleResult);

                if (reader.Read())
                {
                    clsTicketType ticketType = new clsTicketType(
                        Convert.ToInt32(reader["id"]),
                        Convert.ToString(reader["name"]),
                        Convert.ToDecimal(reader["price"]),
                        reader["amount"] != DBNull.Value ? (int?)Convert.ToInt32(reader["amount"]) : null,
                        Convert.ToDateTime(reader["timestamp"])
                        );
                    return ticketType;
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
    }
}
