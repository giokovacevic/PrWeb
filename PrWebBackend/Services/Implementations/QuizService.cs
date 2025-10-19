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
        public List<QuizDTO> GetAllQuizzes()
        {
            List<Quiz> quizes = _quizRepository.ReadAll();
            List<QuizDTO> quizDTOs = new List<QuizDTO>();
            foreach(Quiz quiz in quizes)
            {
                if (quiz != null) quizDTOs.Add(new QuizDTO(quiz));
            }
            return quizDTOs;
        }

        public QuizDTO GetQuizById(int quizId)
        {
            Quiz quiz = _quizRepository.ReadById(quizId);
            QuizDTO quizDTO = null;
            if (quiz != null) quizDTO = new QuizDTO(quiz);
            return quizDTO;
        }

        public QuizResultDTO AddQuizResult(QuizResultRequestDTO quizResultRequestDTO) // TODO:
        {
            // check right and wrong answers
            // create QuizResult.cs instance
            // write it to repo
            // return QuizResultDTO back
            return null;
        }

        public QuizResultDTO GetAllQuizResultsByUserId(int userId) // TODO:
        {
            return null;
        }

        public List<QuizResultDTO> GetAllQuizResults() // TODO:
        {
            return null;
        }
    }
}
