using MySql.Data.MySqlClient;
using PrWebBackend.Models;
using PrWebBackend.Repositories.Interfaces;
using System.Collections.Generic;

namespace PrWebBackend.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly string _connectionString;

        public static readonly Dictionary<string, string> Columns = new Dictionary<string, string>()
        {
            {nameof(Role.Id), "role_id" },
            {nameof(Role.Name), "role_name" },
        };

        public RoleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Role> ReadAll()
        {
            List<Role> roles = new List<Role>();

            string query = $"SELECT {Columns[nameof(Role.Id)]},{Columns[nameof(Role.Name)]} FROM Role;";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal(Columns[nameof(Role.Id)]));
                            string name = reader.GetString(reader.GetOrdinal(Columns[nameof(Role.Name)]));

                            roles.Add(new Role(id, name));
                        }
                    }
                }
            }

            return roles;
        }
    }
}
