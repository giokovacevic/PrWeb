namespace PrWebBackend.Models.Quiz
{
    public class QuizDifficulty
    {
        private int id;
        private string value;

        public QuizDifficulty(int id, string value)
        {
            Id = id;
            Value = value;
        }

        public int Id { get => id; set => id = value; }
        public string Value { get => value; set => this.value = value; }
    }
}
