using PrWebBackend.Models.Quiz;
using System.Collections.Generic;

namespace PrWebBackend.DTOs.Quiz
{
    public class QuizDTO
    {
        private int id;
        private string name;
        private int timeSeconds;
        private QuizDifficultyDTO difficulty;
        private string description;
        private List<QuestionDTO> questions = new List<QuestionDTO>();

        public QuizDTO(Models.Quiz.Quiz quiz)
        {
            Id = quiz.Id;
            Name = quiz.Name;
            TimeSeconds = quiz.TimeSeconds;
            Difficulty = new QuizDifficultyDTO(quiz.Difficulty);
            Description = quiz.Description;
            foreach(Question question in quiz.Questions)
            {
                questions.Add(new QuestionDTO(question));
            }
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int TimeSeconds { get => timeSeconds; set => timeSeconds = value; }
        public QuizDifficultyDTO Difficulty { get => difficulty; set => difficulty = value; }
        public string Description { get => description; set => description = value; }
        public List<QuestionDTO> Questions { get => questions; set => questions = value; }
    }
}
