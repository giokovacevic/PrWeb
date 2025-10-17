using PrWebBackend.Models.Quiz;

namespace PrWebBackend.DTOs.Quiz
{
    public class QuizDifficultyDTO
    {
        private int id;
        private string value;

        public QuizDifficultyDTO(QuizDifficulty difficulty)
        {
            Id = difficulty.Id;
            Value = difficulty.Value;
        }

        public int Id { get => id; set => id = value; }
        public string Value { get => value; set => this.value = value; }
    }
}
