using PrWebBackend.Models.Quiz;

namespace PrWebBackend.DTOs.Quiz
{
    public class QuestionOptionDTO
    {
        private int id;
        private string text;
        private bool isCorrect;
        public QuestionOptionDTO(QuestionOption option)
        {
            Id = option.Id;
            text = option.Text;
            IsCorrect = option.IsCorrect;
        }

        public int Id { get => id; set => id = value; }
        public string Text { get => text; set => text = value; }
        public bool IsCorrect { get => isCorrect; set => isCorrect = value; }
    }
}
