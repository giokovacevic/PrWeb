namespace PrWebBackend.Models
{
    public class User
    {
        private readonly int _id;
        private readonly string _username;
        private readonly string _email;
        private readonly string _password;

        private readonly Role _role;

        public User(int id, string username, string email, string password, Role role)
        {
            _id = id;
            _username = username;
            _email = email;
            _password = password;
            _role = role;
        }

        public int Id => _id;

        public string Username => _username;

        public string Email => _email;

        public string Password => _password;

        public Role Role => _role;
    }
}
