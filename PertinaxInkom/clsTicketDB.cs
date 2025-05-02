using MySqlConnector;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PertinaxInkom.Properties;

namespace PertinaxInkom
{
    public class clsTicketDB
    {
        ADOConnection T = new ADOConnection();

        public clsTicket GetTicketType(int user_id)
        {
           //to implement
           return null;
        }

        public clsTicket CreateTickets(int ticketTypeId, int buyerId, int userId, string ticketUuid)
        {
            // to implement

        return null ;
        }

        public clsTicket? GetTicketByUuid(string ticketUuid)
        {
            MySqlConnection CN = new MySqlConnection(T.Pertinaxlanstr);
            MySqlCommand CMD = new MySqlCommand("S_TicketByUUID", CN);
            CMD.CommandType = CommandType.StoredProcedure;
            CMD.Parameters.AddWithValue("p_Uuid", ticketUuid);

            try
            {
                CN.Open();
                MySqlDataReader reader = CMD.ExecuteReader();

                if (reader.Read())
                {
                    clsTicket ticket = new clsTicket(
                        Convert.ToInt32(reader["id"]),
                        Convert.ToInt32(reader["ticket_type_id"]),
                        Convert.ToInt32(reader["buyer_id"]),
                        Convert.ToInt32(reader["user_id"]),
                        Convert.ToString(reader["ticket_uuid"]),
                        Convert.ToDateTime(reader["order_date"]));

                    return ticket;
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
