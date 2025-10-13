using System.ComponentModel.DataAnnotations;

namespace PrWebBackend.Models
{
    public class Person
    {
        private int id;
        private string name;

        public Person(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
