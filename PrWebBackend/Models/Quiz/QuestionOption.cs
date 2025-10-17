namespace PrWebBackend.Models.Quiz
{
    public class QuestionOption
    {
        private int id;
        private string text;
        private bool isCorrect;

        public QuestionOption(int id, string text, bool isCorrect)
        {
            Id = id;
            Text = text;
            IsCorrect = isCorrect;
        }

        public int Id { get => id; set => id = value; }
        public string Text { get => text; set => text = value; }
        public bool IsCorrect { get => isCorrect; set => isCorrect = value; }
    }
}
