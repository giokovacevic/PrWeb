using Org.BouncyCastle.Asn1.Misc;
using PrWebBackend.Models.Quiz;

namespace PrWebBackend.DTOs.Quiz
{
    public class QuizResultDTO
    {
        private int id;
        private string quizName;
        private string quizDifficultyValue;
        private string userUsername;
        private string date;
        private int time;
        private int answersCount;
        private int correctAnswers;
        private double correctAnswersPercentage;
        private int points;
        private int maxPoints;
        private double pointsPercentage;
        public QuizResultDTO(QuizResult quizResult)
        {
            Id = quizResult.Id;
            QuizName = quizResult.Quiz.Name;
            QuizDifficultyValue = quizResult.Quiz.Difficulty.Value;
            UserUsername = quizResult.UserUsername;
            Date = quizResult.Datetime.ToString("dd:MM:yyyy HH:mm");
            Time = quizResult.TimeNeededSeconds;
            AnswersCount = quizResult.Quiz.Questions.Count;
            CorrectAnswers = quizResult.CorrectAnswers;
            CorrectAnswersPercentage = ((double)CorrectAnswers / (double)AnswersCount) * 100;
            Points = quizResult.Points;
            MaxPoints = 5 * AnswersCount;
            PointsPercentage = points>0 ? (Points / (double)MaxPoints) * 100 : 0;
        }

        public string QuizName { get => quizName; set => quizName = value; }
        public string QuizDifficultyValue { get => quizDifficultyValue; set => quizDifficultyValue = value; }
        public string UserUsername { get => userUsername; set => userUsername = value; }
        public int AnswersCount { get => answersCount; set => answersCount = value; }
        public int CorrectAnswers { get => correctAnswers; set => correctAnswers = value; }
        public double CorrectAnswersPercentage { get => correctAnswersPercentage; set => correctAnswersPercentage = value; }
        public int Points { get => points; set => points = value; }
        public int MaxPoints { get => maxPoints; set => maxPoints = value; }
        public double PointsPercentage { get => pointsPercentage; set => pointsPercentage = value; }
        public int Id { get => id; set => id = value; }
        public string Date { get => date; set => date = value; }
        public int Time { get => time; set => time = value; }
    }
}
