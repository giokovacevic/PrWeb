using PrWebBackend.DTOs.Quiz;
using System.Collections.Generic;

namespace PrWebBackend.Services.Interfaces
{
    public interface IQuizService
    {
        public List<QuizDTO> GetAll();
        public QuizDTO GetById(int id);
    }
}
