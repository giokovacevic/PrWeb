using PrWebBackend.Models.Quiz;
using System.Collections.Generic;

namespace PrWebBackend.Repositories.Interfaces
{
    public interface IQuizRepository
    {
        public List<Quiz> ReadAllQuizzes();
        public Quiz ReadQuizById(int id);
        public void CreateQuizResult(QuizResult quizResult);
        public List<QuizResult> ReadAllQuizResultsByUserId(int userId);
    }
}
