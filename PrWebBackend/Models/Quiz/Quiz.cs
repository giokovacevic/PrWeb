using System.Collections.Generic;

namespace PrWebBackend.Models.Quiz
{
    public class Quiz
    {
        private int id;
        private string name;
        private int timeSeconds;
        private QuizDifficulty difficulty;
        private string description;
        private List<Question> questions = new List<Question>();

        public Quiz(int id, string name, int timeSeconds, QuizDifficulty difficulty, string description)
        {
            Id = id;
            Name = name;
            TimeSeconds = timeSeconds;
            Difficulty = difficulty;
            Description = description;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int TimeSeconds { get => timeSeconds; set => timeSeconds = value; }
        public QuizDifficulty Difficulty { get => difficulty; set => difficulty = value; }
        public string Description { get => description; set => description = value; }
        public List<Question> Questions { get => questions; set => questions = value; }
    }
}
