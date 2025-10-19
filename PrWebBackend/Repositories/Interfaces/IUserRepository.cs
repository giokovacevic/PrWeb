using PrWebBackend.Models.NamespaceUser;
using System.Collections.Generic;

namespace PrWebBackend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public List<User> ReadAll();
        public User ReadById(int id);
        public User ReadByUsername(string username);
        public User ReadByEmail(string email);
        public User ReadByUsernameOrEmail(string usernameOrEmail);
        public void CreateUser(User user);
    }
}
