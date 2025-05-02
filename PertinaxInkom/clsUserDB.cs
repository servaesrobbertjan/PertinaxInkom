using MySqlConnector;
using PertinaxInkom.Properties;
using Swelio.Engine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PertinaxInkom
{
    public class clsUserDB
    {
        ADOConnection A = new ADOConnection();

        public ObservableCollection<clsUser> GetUsers()
        {
            MySqlConnection CN = new MySqlConnection(A.Pertinaxlanstr);
            MySqlCommand CMD = new MySqlCommand("S_Accounts", CN);
            CMD.CommandType = CommandType.StoredProcedure;
            ObservableCollection<clsUser> users = new ObservableCollection<clsUser>();

            try
            {
                CN.Open();
                MySqlDataReader reader = CMD.ExecuteReader();
                while (reader.Read())
                {
                    clsUser user = new clsUser(
                        Convert.ToInt32(reader["id"]),
                        Convert.ToInt32(reader["address_id"]),
                        Convert.ToInt32(reader["wallets_id"]),
                        Convert.ToString(reader["nick_name"]),
                        Convert.ToString(reader["password"]),
                        Convert.ToString(reader["first_name"]),
                        Convert.ToString(reader["last_name"]),
                        Convert.ToString(reader["email"]),
                        Convert.ToString(reader["uuid"]),
                        Convert.ToDateTime(reader["birth_date"]),
                        Convert.ToDateTime(reader["timestamp"]));
                    users.Add(user);
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
            return users;
        }

        public clsUser? GetUserByNickName(string nickname)
        {
            MySqlConnection CN = new MySqlConnection(A.Pertinaxlanstr);
            MySqlCommand CMD = new MySqlCommand(Properties.StoredProcedures.S_AccountByUsername, CN);
            CMD.CommandType = CommandType.StoredProcedure;
            CMD.Parameters.AddWithValue("username", nickname);

            try
            {
                CN.Open();
                MySqlDataReader reader = CMD.ExecuteReader();

                if (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    int addressId = reader.IsDBNull(reader.GetOrdinal("address_id")) ? 0 : Convert.ToInt32(reader["address_id"]);
                    int walletId = reader.IsDBNull(reader.GetOrdinal("wallet_id")) ? 0 : Convert.ToInt32(reader["wallet_id"]);
                    string nickName = Convert.ToString(reader["nick_name"]);
                    string password = Convert.ToString(reader["password"]);
                    string firstName = Convert.ToString(reader["first_name"]);
                    string lastName = Convert.ToString(reader["last_name"]);
                    string email = reader.IsDBNull(reader.GetOrdinal("email")) ? string.Empty : Convert.ToString(reader["email"]);
                    string uuid = reader.IsDBNull(reader.GetOrdinal("uuid")) ? string.Empty : Convert.ToString(reader["uuid"]);
                    DateTime? birthDate = reader.IsDBNull(reader.GetOrdinal("birth_date"))? (DateTime?)null : Convert.ToDateTime(reader["birth_date"]);
                    DateTime timestamp = Convert.ToDateTime(reader["timestamp"]);


                    clsUser user = new clsUser(
                        id,
                        addressId,
                        walletId,
                        nickName,
                        password,
                        firstName,
                        lastName,
                        email,
                        uuid,
                        birthDate,
                        timestamp
                    );

                    return user;
                }
                else
                {
                    return null;
                }
            } 
            finally
            {
                CN.Close();
            }
        }

        public clsUser? GetUserByName(string firstName, string lastName)
        {
            MySqlConnection CN = new MySqlConnection(A.Pertinaxlanstr);
            MySqlCommand CMD = new MySqlCommand("S_AccountByName", CN);
            CMD.CommandType = CommandType.StoredProcedure;
            CMD.Parameters.AddWithValue("firstName", firstName);
            CMD.Parameters.AddWithValue("lastName", lastName);

            try
            {
                CN.Open();
                MySqlDataReader reader = CMD.ExecuteReader();

                if (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    int addressId = reader.IsDBNull(reader.GetOrdinal("address_id")) ? 0 : Convert.ToInt32(reader["address_id"]);
                    int walletId = reader.IsDBNull(reader.GetOrdinal("wallets_id")) ? 0 : Convert.ToInt32(reader["wallets_id"]);
                    string? nickName = reader.IsDBNull(reader.GetOrdinal("nick_name")) ? (string?)null : Convert.ToString(reader["nick_name"]);
                    string password = Convert.ToString(reader["password"]);
                    string email = reader.IsDBNull(reader.GetOrdinal("email")) ? string.Empty : Convert.ToString(reader["email"]);
                    string uuid = reader.IsDBNull(reader.GetOrdinal("uuid")) ? string.Empty : Convert.ToString(reader["uuid"]);
                    DateTime? birthDate = reader.IsDBNull(reader.GetOrdinal("birth_date")) ? (DateTime?)null : Convert.ToDateTime(reader["birth_date"]);
                    DateTime timestamp = Convert.ToDateTime(reader["timestamp"]);


                    clsUser user = new clsUser(
                        id,
                        addressId,
                        walletId,
                        nickName,
                        password,
                        firstName,
                        lastName,
                        email,
                        uuid,
                        birthDate,
                        timestamp
                    );

                    return user;
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                CN.Close();
            }
        }

        public clsUser GetUserById(int id)
        {
            MySqlConnection CN = new MySqlConnection(A.Pertinaxlanstr);
            MySqlCommand CMD = new MySqlCommand(Properties.StoredProcedures.S_AccountById, CN);
            CMD.CommandType = CommandType.StoredProcedure;
            CMD.Parameters.AddWithValue("accountId", id);

            try
            {
                CN.Open();
                MySqlDataReader reader = CMD.ExecuteReader();

                if (reader.Read())
                {
                    int addressId = reader.IsDBNull(reader.GetOrdinal("address_id")) ? 0 : Convert.ToInt32(reader["address_id"]);
                    int walletId = reader.IsDBNull(reader.GetOrdinal("wallet_id")) ? 0 : Convert.ToInt32(reader["wallet_id"]);
                    string nickName = Convert.ToString(reader["nick_name"]);
                    string password = Convert.ToString(reader["password"]);
                    string firstName = Convert.ToString(reader["first_name"]);
                    string lastName = Convert.ToString(reader["last_name"]);
                    string email = reader.IsDBNull(reader.GetOrdinal("email")) ? string.Empty : Convert.ToString(reader["email"]);
                    string uuid = reader.IsDBNull(reader.GetOrdinal("barcode")) ? string.Empty : Convert.ToString(reader["barcode"]);
                    DateTime? birthDate = reader.IsDBNull(reader.GetOrdinal("birth_date")) ? (DateTime?)null : Convert.ToDateTime(reader["birth_date"]);
                    DateTime timestamp = Convert.ToDateTime(reader["timestamp"]);

                    clsUser user = new clsUser(
                        id,
                        addressId,
                        walletId,
                        nickName,
                        password,
                        firstName,
                        lastName,
                        email,
                        uuid,
                        birthDate,
                        timestamp

                    );
                    return user;
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

        public clsUser GetUserByUuid(string Uuid)
        {
            MySqlConnection CN = new MySqlConnection(A.Pertinaxlanstr);
            MySqlCommand CMD = new MySqlCommand(Properties.StoredProcedures.S_AccountByUUID, CN);
            CMD.CommandType = CommandType.StoredProcedure;
            CMD.Parameters.AddWithValue("p_uuid", Uuid);

            try
            {
                CN.Open();
                MySqlDataReader reader = CMD.ExecuteReader();

                if (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    int addressId = reader.IsDBNull(reader.GetOrdinal("address_id")) ? 0 : Convert.ToInt32(reader["address_id"]);
                    int walletId = reader.IsDBNull(reader.GetOrdinal("wallet_id")) ? 0 : Convert.ToInt32(reader["wallet_id"]);
                    string nickName = Convert.ToString(reader["nick_name"]);
                    string password = Convert.ToString(reader["password"]);
                    string firstName = Convert.ToString(reader["first_name"]);
                    string lastName = Convert.ToString(reader["last_name"]);
                    string email = reader.IsDBNull(reader.GetOrdinal("email")) ? string.Empty : Convert.ToString(reader["email"]);
                    string uuid = reader.IsDBNull(reader.GetOrdinal("uuid")) ? string.Empty : Convert.ToString(reader["uuid"]);
                    DateTime? birthDate = reader.IsDBNull(reader.GetOrdinal("birth_date")) ? (DateTime?)null : Convert.ToDateTime(reader["birth_date"]);
                    DateTime timestamp = Convert.ToDateTime(reader["timestamp"]);

                    clsUser user = new clsUser(
                        id,
                        addressId,
                        walletId,
                        nickName,
                        password,
                        firstName,
                        lastName,
                        email,
                        uuid,
                        birthDate,
                        timestamp

                    );
                    return user;
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

        public bool UpdateUser(int id, int? address, int? wallet, string nickname, string? password,
                string first_name, string last_name, string? email, string Uuid, DateTime? birth_date)
        {
            try
            {
                MySqlConnection CN = new MySqlConnection(A.Pertinaxlanstr);
                MySqlCommand CMD = new MySqlCommand(Properties.StoredProcedures.U_Account, CN);
                CMD.CommandType = CommandType.StoredProcedure;
                CMD.Parameters.AddWithValue("p_id", id);
                if (address == 0)
                {
                    CMD.Parameters.AddWithValue("p_address_id", DBNull.Value);
                }
                else
                {
                    CMD.Parameters.AddWithValue("p_address_id", address); ;
                }
                if (wallet == 0 || wallet == null)
                {
                    CMD.Parameters.AddWithValue("p_walllet_id", DBNull.Value);
                }
                else
                {
                    CMD.Parameters.AddWithValue("p_wallet_id", wallet); ;
                }
                CMD.Parameters.AddWithValue("p_nick_name", nickname);
                if (password == string.Empty)
                {
                    CMD.Parameters.AddWithValue("p_password", BCrypt.Net.BCrypt.HashPassword(first_name));
                }
                else
                {
                    CMD.Parameters.AddWithValue("p_password", password);
                }
                CMD.Parameters.AddWithValue("p_first_name", first_name);
                CMD.Parameters.AddWithValue("p_last_name", last_name);
                if (string.IsNullOrEmpty(email))
                {
                    CMD.Parameters.AddWithValue("p_email", DBNull.Value);
                }
                else
                {
                    CMD.Parameters.AddWithValue("p_email", email);
                }
                CMD.Parameters.AddWithValue("p_uuid", Uuid);
                CMD.Parameters.AddWithValue("p_birth_date", birth_date);

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

        public int? CreateUser(int? address, int? wallet, string nickname, string? password,
                string first_name, string last_name, string? email, string Uuid, DateTime birth_date)
        {
            try
            {
                MySqlConnection CN = new MySqlConnection(A.Pertinaxlanstr);
                MySqlCommand CMD = new MySqlCommand(Properties.StoredProcedures.C_Account, CN);
                CMD.CommandType = CommandType.StoredProcedure;
                CMD.Parameters.AddWithValue("p_address_id", address);
                CMD.Parameters.AddWithValue("p_wallet_id", wallet); ;
                CMD.Parameters.AddWithValue("p_nick_name", nickname);
                CMD.Parameters.AddWithValue("p_password", password);
                CMD.Parameters.AddWithValue("p_first_name", first_name);
                CMD.Parameters.AddWithValue("p_last_name", last_name);
                CMD.Parameters.AddWithValue("p_email", email);
                CMD.Parameters.AddWithValue("p_uuid", Uuid);
                CMD.Parameters.AddWithValue("p_birth_date", birth_date);

                CN.Open();
                object result = CMD.ExecuteScalar();
                CN.Close();
                
                return result != null ? Convert.ToInt32(result) : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
