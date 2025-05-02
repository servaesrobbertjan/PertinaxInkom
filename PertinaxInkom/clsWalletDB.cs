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
    public class clsWalletDB
    {
        ADOConnection W = new ADOConnection();

        public clsWallet GetWallet(int Id)
        {
            MySqlConnection CN = new MySqlConnection(W.Pertinaxlanstr);
            MySqlCommand CMD = new MySqlCommand(Properties.StoredProcedures.S_Wallet, CN);
            CMD.CommandType = CommandType.StoredProcedure;
            CMD.Parameters.AddWithValue("p_id", Id);

            try
            {
                CN.Open();
                MySqlDataReader reader = CMD.ExecuteReader();

                if (reader.Read())
                {
                    clsWallet wallet = new clsWallet(
                        Convert.ToInt32(reader["id"]),
                        Convert.ToDecimal(reader["balans"]),
                        Convert.ToInt32(reader["pincode"]));
                    return wallet;
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

        public bool UpdateWallet(int  id, decimal balans, int pincode)
        {
            try
            {
                MySqlConnection CN = new MySqlConnection(W.Pertinaxlanstr);
                MySqlCommand CMD = new MySqlCommand(Properties.StoredProcedures.U_Wallet, CN);
                CMD.CommandType = CommandType.StoredProcedure;
                CMD.Parameters.AddWithValue("p_id", id);
                CMD.Parameters.AddWithValue("p_balans", balans);
                CMD.Parameters.AddWithValue("p_pincode", pincode);

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

        public int CreateWallet(decimal balans, int? pincode)
        {
            try
            {
                MySqlConnection CN = new MySqlConnection(W.Pertinaxlanstr);
                MySqlCommand CMD = new MySqlCommand(Properties.StoredProcedures.C_Wallet, CN);
                CMD.CommandType = CommandType.StoredProcedure;
                CMD.Parameters.AddWithValue("p_balans", balans);
                CMD.Parameters.AddWithValue("p_pincode", pincode);
               

                CN.Open();
                int walletid = Convert.ToInt32(CMD.ExecuteScalar());
                CN.Close();

                return walletid;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
