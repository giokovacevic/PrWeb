namespace PrWebBackend.Models.Quiz
{
    public class TrueFalseQuestion : Question
    {
        private bool isCorrect;

        public TrueFalseQuestion(int id, string text, string theme, bool isCorrect) : base(id, text, theme)
        {
            IsCorrect = isCorrect;
        }

        public bool IsCorrect { get => isCorrect; set => isCorrect = value; }

        public override QuestionType GetQuestionType()
        {
            return QuestionType.TRUE_FALSE;
        }
    }
}
