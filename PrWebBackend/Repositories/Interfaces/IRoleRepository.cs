using PrWebBackend.Models;
using System.Collections.Generic;

namespace PrWebBackend.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        public List<Role> ReadAll();
    }
}
