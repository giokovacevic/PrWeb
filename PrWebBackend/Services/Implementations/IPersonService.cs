using PrWebBackend.DTOs;
using PrWebBackend.Models;
using System.Collections.Generic;

namespace PrWebBackend.Services.Implementations
{
    public interface IPersonService
    {
        public List<PersonDTO> GetAll();
    }
}
