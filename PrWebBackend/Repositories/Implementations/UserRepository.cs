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

        public static readonly Dictionary<string, string> Columns = new Dictionary<string, string>()
        {
            {nameof(User.Id), "user_id" },
            {nameof(User.Username), "user_username" },
            {nameof(User.Email), "user_email" },
            {nameof(User.Password), "user_password" },
            {nameof(User.ImageUrl), "user_imageurl" }
        };

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

            string userAlias = "u";
            string roleAlias = "r";

            string query = $"SELECT {Columns[nameof(User.Id)]}, {Columns[nameof(User.Username)]}, {Columns[nameof(User.Email)]}, {Columns[nameof(User.Password)]}, {Columns[nameof(User.ImageUrl)]}, {roleAlias}.{RoleRepository.Columns[nameof(Role.Id)]} AS {RoleRepository.Columns[nameof(Role.Id)]} , {roleAlias}.{RoleRepository.Columns[nameof(Role.Name)]} AS {RoleRepository.Columns[nameof(Role.Name)]} FROM User {userAlias} INNER JOIN Role {roleAlias} on {userAlias}.{RoleRepository.Columns[nameof(Role.Id)]} = {roleAlias}.{RoleRepository.Columns[nameof(Role.Id)]};";

            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(Columns[nameof(User.Id)]);
                            string username = reader.GetString(Columns[nameof(User.Username)]);
                            string email = reader.GetString(Columns[nameof(User.Email)]);
                            string password = reader.GetString(Columns[nameof(User.Password)]);
                            string imageUrl = reader.IsDBNull(reader.GetOrdinal(Columns[nameof(User.ImageUrl)])) ? null : reader.GetString(Columns[nameof(User.ImageUrl)]);
                            int roleId = reader.GetInt32(RoleRepository.Columns[nameof(Role.Id)]);
                            string roleName = reader.GetString(RoleRepository.Columns[nameof(Role.Name)]);
                            
                            User user = new User(id, username, email, password, imageUrl, new Role(roleId, roleName));

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
