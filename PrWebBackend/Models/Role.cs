namespace PrWebBackend.Models
{
    public class Role
    {
        private readonly int _id;
        private readonly string _name;

        public Role(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public int Id => _id;

        public string Name => _name;
    }
}
