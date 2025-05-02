using MySqlConnector;
using PertinaxInkom.Properties;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PertinaxInkom
{
    public class clsRoleDB
    {
        ADOConnection R = new ADOConnection();
        public ObservableCollection<clsRole> GetRoles() 
        {
            MySqlConnection CN = new MySqlConnection(R.Pertinaxlanstr);
            MySqlCommand CMD = new MySqlCommand("S_Roles", CN);
            CMD.CommandType = CommandType.StoredProcedure;
            ObservableCollection<clsRole> roles = new ObservableCollection<clsRole>();

            try
            {
                CN.Open();
                MySqlDataReader reader = CMD.ExecuteReader();
                while (reader.Read())
                {
                    clsRole role = new clsRole(
                        Convert.ToInt32(reader["id"]),
                        Convert.ToString(reader["roleName"]),
                        Convert.ToDateTime(reader["timestamp"]));
                    roles.Add(role);
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
            return roles;
        }

        public clsRole GetRoleById(int id)
        {
            MySqlConnection CN = new MySqlConnection(R.Pertinaxlanstr);
            MySqlCommand CMD = new MySqlCommand("S_RolesById", CN);
            CMD.CommandType = CommandType.StoredProcedure;
            CMD.Parameters.AddWithValue("id", id);

            try
            {
                CN.Open();
                MySqlDataReader reader = CMD.ExecuteReader();

                if (reader.Read())
                {
                    string roleName = Convert.ToString(reader["role_name"]);
                    DateTime timestamp = Convert.ToDateTime(reader["timestamp"]);

                    clsRole role = new clsRole(
                        id,
                        roleName,
                        timestamp
                     );

                    return role;
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

        public clsRole GetRoleByName(string roleName)
        {
            MySqlConnection CN = new MySqlConnection(R.Pertinaxlanstr);
            MySqlCommand CMD = new MySqlCommand("S_RolesByName", CN);
            CMD.CommandType = CommandType.StoredProcedure;
            CMD.Parameters.AddWithValue("rolename", roleName);

            try
            {
                CN.Open();
                MySqlDataReader reader = CMD.ExecuteReader();

                if (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    DateTime timestamp = Convert.ToDateTime(reader["timestamp"]);

                    clsRole role = new clsRole(
                        id,
                        roleName,
                        timestamp
                     );

                    return role;
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
    }
}
