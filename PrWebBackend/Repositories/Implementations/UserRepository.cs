using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using PrWebBackend.Models;
using PrWebBackend.Repositories.Interfaces;
using System;
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

        public bool CreateUser(User user)
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
                            users.Add(ExtractUserFromReader(reader));
                        }
                    }
                }
            }

            return users;
        }

        public User ReadByUsernameOrEmail(string usernameOrEmail)
        {
            User user = null;

            string userAlias = "u";
            string roleAlias = "r";

            string query = $"SELECT {Columns[nameof(User.Id)]}, {Columns[nameof(User.Username)]}, {Columns[nameof(User.Email)]}, {Columns[nameof(User.Password)]}, {Columns[nameof(User.ImageUrl)]}, {roleAlias}.{RoleRepository.Columns[nameof(Role.Id)]} AS {RoleRepository.Columns[nameof(Role.Id)]} , {roleAlias}.{RoleRepository.Columns[nameof(Role.Name)]} AS {RoleRepository.Columns[nameof(Role.Name)]} FROM User {userAlias} INNER JOIN Role {roleAlias} on {userAlias}.{RoleRepository.Columns[nameof(Role.Id)]} = {roleAlias}.{RoleRepository.Columns[nameof(Role.Id)]} WHERE {Columns[nameof(User.Username)]} = @value OR {Columns[nameof(User.Email)]} = @value;";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@value", usernameOrEmail);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            user = ExtractUserFromReader(reader);
                        }
                    }
                }
            }

            return user;
        }

        public User ReadByEmail(string email)
        {
            List<User> users = ReadByField(nameof(User.Email), email);
            if(users.Count > 0)
            {
                return users[0];
            }
            return null;
        }

        public User ReadById(int id)
        {
            List<User> users = ReadByField(nameof(User.Id), id);
            if (users.Count > 0)
            {
                return users[0];
            }
            return null;
        }

        public User ReadByUsername(string username)
        {
            List<User> users = ReadByField(nameof(User.Username), username);
            if (users.Count > 0)
            {
                return users[0];
            }
            return null;
        }

        private List<User> ReadByField(string fieldName, object value)
        {
            List<User> users = new List<User>();

            string userAlias = "u";
            string roleAlias = "r";

            string query = $"SELECT {Columns[nameof(User.Id)]}, {Columns[nameof(User.Username)]}, {Columns[nameof(User.Email)]}, {Columns[nameof(User.Password)]}, {Columns[nameof(User.ImageUrl)]}, {roleAlias}.{RoleRepository.Columns[nameof(Role.Id)]} AS {RoleRepository.Columns[nameof(Role.Id)]} , {roleAlias}.{RoleRepository.Columns[nameof(Role.Name)]} AS {RoleRepository.Columns[nameof(Role.Name)]} FROM User {userAlias} INNER JOIN Role {roleAlias} on {userAlias}.{RoleRepository.Columns[nameof(Role.Id)]} = {roleAlias}.{RoleRepository.Columns[nameof(Role.Id)]} WHERE {Columns[fieldName]} = @value;";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@value", value);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            users.Add(ExtractUserFromReader(reader));
                        }
                    }
                }
            }

            return users;
        }

        private User ExtractUserFromReader(MySqlDataReader reader)
        {
            int id = reader.GetInt32(reader.GetOrdinal(Columns[nameof(User.Id)]));
            string username = reader.GetString(reader.GetOrdinal(Columns[nameof(User.Username)]));
            string email = reader.GetString(reader.GetOrdinal(Columns[nameof(User.Email)]));
            string password = reader.GetString(reader.GetOrdinal(Columns[nameof(User.Password)]));
            string imageUrl = reader.IsDBNull(reader.GetOrdinal(Columns[nameof(User.ImageUrl)])) ? null : reader.GetString(reader.GetOrdinal(Columns[nameof(User.ImageUrl)]));
            int roleId = reader.GetInt32(reader.GetOrdinal(RoleRepository.Columns[nameof(Role.Id)]));
            string roleName = reader.GetString(reader.GetOrdinal(RoleRepository.Columns[nameof(Role.Name)]));

            return new User(id, username, email, password, imageUrl, new Role(roleId, roleName));
        }
    }
}
