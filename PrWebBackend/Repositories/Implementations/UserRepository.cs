using MySql.Data.MySqlClient;
using PrWebBackend.Models;
using PrWebBackend.Repositories.Interfaces;
using System.Collections.Generic;
using System.Configuration;

namespace PrWebBackend.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<User> CreateUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public List<User> ReadAll()
        {
            List<User> users = new List<User>();

            string query = "SELECT user_id, user_username, user_email, user_password, role_id, role_name FROM user u INNER JOIN Role r on u.user_role_id = r.role_id;";

            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("user_id");
                            string username = reader.GetString("user_username");
                            string email = reader.GetString("user_email");
                            string password = reader.GetString("user_password");
                            int roleId = reader.GetInt32("role_id");
                            string roleName = reader.GetString("role_name");
                            
                            User user = new User(id, username, email, password, new Role(roleId, roleName));

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        public List<User> ReadByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public List<User> ReadById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<User> ReadByUsername(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}
