using PrWebBackend.Models;
using System.Collections.Generic;

namespace PrWebBackend.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        public List<Person> ReadAll();
    }
}
