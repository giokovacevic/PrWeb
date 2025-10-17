using PrWebBackend.Models.Quiz;
using System.Collections.Generic;

namespace PrWebBackend.Repositories.Interfaces
{
    public interface IQuizRepository
    {
        public List<Quiz> ReadAll();
        public Quiz ReadById(int id);
    }
}
