using PrWebBackend.Models;
using System.Collections.Generic;

namespace PrWebBackend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public List<User> ReadAll();
        public List<User> ReadById(int id);
        public List<User> ReadByUsername(string username);
        public List<User> ReadByEmail(string email);
        public List<User> CreateUser(User user);
    }
}
