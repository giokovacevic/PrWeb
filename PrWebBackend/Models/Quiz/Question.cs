namespace PrWebBackend.Models.Quiz
{
    public enum QuestionType
    {
        FILL_IN,
        TRUE_FALSE,
        MULTI_CHOICE,
        SINGLE_CHOICE
    }
    public abstract class Question
    {
        private int id;
        private string text;
        private string theme;

        public Question(int id, string text, string theme)
        {
            Id = id;
            Text = text;
            Theme = theme;
        }

        public int Id { get => id; set => id = value; }
        public string Text { get => text; set => text = value; }
        public string Theme { get => theme; set => theme = value; }

        public abstract QuestionType GetQuestionType();
    }
}
