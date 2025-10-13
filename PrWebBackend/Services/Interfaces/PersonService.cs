using PrWebBackend.DTOs;
using PrWebBackend.Models;
using PrWebBackend.Repositories.Interfaces;
using PrWebBackend.Services.Implementations;
using System.Collections.Generic;

namespace PrWebBackend.Services.Interfaces
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            this._personRepository = personRepository;
        }
        public List<PersonDTO> GetAll()
        {
            List<PersonDTO> personDTOs = new List<PersonDTO>();
            List<Person> persons = _personRepository.ReadAll();
            foreach(Person person in persons)
            {
                if (person != null) personDTOs.Add(new PersonDTO(person));
            }
            return personDTOs;
        }
    }
}
