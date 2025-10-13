using MySql.Data.MySqlClient;
using PrWebBackend.Models;
using PrWebBackend.Repositories.Interfaces;
using System.Collections.Generic;

namespace PrWebBackend.Repositories.Implementations
{
    public class PersonRepository : IPersonRepository
    {
        private readonly string connectionString;

        public PersonRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Person> ReadAll()
        {
            List<Person> persons = new List<Person>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT person_id,person_name FROM Person;";

                using(MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("person_id");
                            string name = reader.GetString("person_name");
                            Person person = new Person(id, name);
                            
                            persons.Add(person);
                        }
                    }
                }
            }

            return persons;
        }
    }
}
