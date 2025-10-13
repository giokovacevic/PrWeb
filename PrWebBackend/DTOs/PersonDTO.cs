using PrWebBackend.Models;
using System;

namespace PrWebBackend.DTOs
{
    public class PersonDTO
    {
        private int id;
        private string name;

        public PersonDTO() { }

        public PersonDTO(Person person)
        {
            if (person == null) throw new ArgumentNullException(nameof(person), "Person cannot be NULL");
            
            this.id = person.Id;
            this.name = person.Name;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
