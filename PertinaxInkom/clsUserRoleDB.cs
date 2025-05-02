using MySqlConnector;
using PertinaxInkom.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PertinaxInkom
{
    public class clsUserRoleDB
    {

        ADOConnection UR = new ADOConnection();

        public ObservableCollection<clsUserRole> GetUserRolesByUser(int userId)
        {
            MySqlConnection CN = new MySqlConnection(UR.Pertinaxlanstr);
            MySqlCommand CMD = new MySqlCommand("S_UserRolesByUser", CN);
            CMD.CommandType = CommandType.StoredProcedure;
            CMD.Parameters.AddWithValue("userId", userId);
            ObservableCollection<clsUserRole> userRoles = new ObservableCollection<clsUserRole>();

            try
            {
                CN.Open();
                MySqlDataReader reader = CMD.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    int rolesId = Convert.ToInt32(reader["roles_id"]);
                    DateTime createdAt = Convert.ToDateTime(reader["created_at"]);
                    DateTime? updatedAt = reader.IsDBNull(reader.GetOrdinal("updated_at")) ? (DateTime?)null : Convert.ToDateTime(reader["updated_at"]);

                    clsUserRole userRole = new clsUserRole(
                        id,
                        userId,
                        rolesId,
                        createdAt,
                        updatedAt);
                   userRoles.Add(userRole);
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
            return userRoles;
        }

        public int CreateUserRole(int userId, int roleId)
        {
            try
            {
                MySqlConnection CN = new MySqlConnection(UR.Pertinaxlanstr);
                MySqlCommand CMD = new MySqlCommand("C_UserRole", CN);
                CMD.CommandType = CommandType.StoredProcedure;
                CMD.Parameters.AddWithValue("userId", userId);
                CMD.Parameters.AddWithValue("rolesId", roleId);

                CN.Open();
                int Id = Convert.ToInt32(CMD.ExecuteScalar());
                CN.Close();

                return Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateUserRole(int id, int rolesId)
        {
            try
            {
                MySqlConnection CN = new MySqlConnection(UR.Pertinaxlanstr);
                MySqlCommand CMD = new MySqlCommand("U_UserRole", CN);
                CMD.CommandType = CommandType.StoredProcedure;
                CMD.Parameters.AddWithValue("p_id", id);
                CMD.Parameters.AddWithValue("p_roles_id", rolesId);

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
