using Microsoft.VisualBasic;
using PrWebBackend.Models.NamespaceUser;
using System;

namespace PrWebBackend.Models.Quiz
{
    public class QuizResult
    {
        private int id;
        private Quiz quiz;
        private int quizId;
        private string userUsername;
        private int userId;
        private DateTime datetime;
        private int timeNeededSeconds;
        private int correctAnswers;
        private int points;

        public QuizResult(int id, Quiz quiz, int quizId, string userUsername, int userId, DateTime datetime, int timeNeededSeconds, int correctAnswers, int points)
        {
            Id = id;
            Quiz = quiz;
            QuizId = quizId;
            UserUsername = userUsername;
            UserId = userId;
            Datetime = datetime;
            TimeNeededSeconds = timeNeededSeconds;
            CorrectAnswers = correctAnswers;
            Points = points;
        }

        public Quiz Quiz { get => quiz; set => quiz = value; }
        public int QuizId { get => quizId; set => quizId = value; }
        public string UserUsername { get => userUsername; set => userUsername = value; }
        public int UserId { get => userId; set => userId = value; }
        public DateTime Datetime { get => datetime; set => datetime = value; }
        public int CorrectAnswers { get => correctAnswers; set => correctAnswers = value; }
        public int Points { get => points; set => points = value; }
        public int Id { get => id; set => id = value; }
        public int TimeNeededSeconds { get => TimeNeededSeconds1; set => TimeNeededSeconds1 = value; }
        public int TimeNeededSeconds1 { get => timeNeededSeconds; set => timeNeededSeconds = value; }
    }
}
