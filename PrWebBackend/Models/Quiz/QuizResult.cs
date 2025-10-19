using Microsoft.VisualBasic;

namespace PrWebBackend.Models.Quiz
{
    public class QuizResult
    {
        private Quiz quiz;
        private int quizId;
        private User user;
        private int userId;
        private DateAndTime datetime;
        private int correctQuestions;
        private int points;

        public QuizResult(Quiz quiz, int quizId, User user, int userId, DateAndTime datetime, int correctQuestions, int points)
        {
            Quiz = quiz;
            QuizId = quizId;
            User = user;
            UserId = userId;
            Datetime = datetime;
            CorrectQuestions = correctQuestions;
            Points = points;
        }

        public Quiz Quiz { get => quiz; set => quiz = value; }
        public int QuizId { get => quizId; set => quizId = value; }
        public User User { get => user; set => user = value; }
        public int UserId { get => userId; set => userId = value; }
        public DateAndTime Datetime { get => datetime; set => datetime = value; }
        public int CorrectQuestions { get => correctQuestions; set => correctQuestions = value; }
        public int Points { get => points; set => points = value; }
    }
}
