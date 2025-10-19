using PrWebBackend.DTOs.Quiz;
using System.Collections.Generic;

namespace PrWebBackend.Services.Interfaces
{
    public interface IQuizService
    {
        public List<QuizDTO> GetAllQuizzes();
        public QuizDTO GetQuizById(int quizId);
        public QuizResultDTO AddQuizResult(QuizResultRequestDTO quizResultRequestDTO);
        public List<QuizResultDTO> GetAllQuizResultsByUserId(int userId);
        public List<QuizResultDTO> GetAllQuizResults();
    }
}
