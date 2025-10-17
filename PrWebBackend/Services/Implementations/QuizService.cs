using PrWebBackend.DTOs.Quiz;
using PrWebBackend.Models.Quiz;
using PrWebBackend.Repositories.Interfaces;
using PrWebBackend.Services.Interfaces;
using System.Collections.Generic;

namespace PrWebBackend.Services.Implementations
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }
        public List<QuizDTO> GetAll()
        {
            List<Quiz> quizes = _quizRepository.ReadAll();
            List<QuizDTO> quizDTOs = new List<QuizDTO>();
            foreach(Quiz quiz in quizes)
            {
                if (quiz != null) quizDTOs.Add(new QuizDTO(quiz));
            }
            return quizDTOs;
        }

        public QuizDTO GetById(int id)
        {
            Quiz quiz = _quizRepository.ReadById(id);
            QuizDTO quizDTO = null;
            if (quiz != null) quizDTO = new QuizDTO(quiz);
            return quizDTO;
        }
    }
}
