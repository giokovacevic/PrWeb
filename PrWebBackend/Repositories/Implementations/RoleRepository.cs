using MySql.Data.MySqlClient;
using PrWebBackend.Models;
using PrWebBackend.Repositories.Interfaces;
using System.Collections.Generic;

namespace PrWebBackend.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly string _connectionString;

        public RoleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Role> ReadAll()
        {
            List<Role> roles = new List<Role>();

            string query = "SELECT role_id, role_name FROM Role;";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            int id = reader.GetInt32("role_id");
                            string name = reader.GetString("role_name");

                            roles.Add(new Role(id, name));
                        }
                    }
                }
            }

            return roles;
        }
    }
}
