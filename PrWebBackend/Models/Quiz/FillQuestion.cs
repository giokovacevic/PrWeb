namespace PrWebBackend.Models.Quiz
{
    public class FillQuestion : Question
    {
        private string answer;

        public FillQuestion(int id, string text, string theme, string answer) : base(id, text, theme)
        {
            Answer = answer;
        }

        public string Answer { get => answer; set => answer = value; }

        public override QuestionType GetQuestionType()
        {
            return QuestionType.FILL_IN;
        }
    }
}
