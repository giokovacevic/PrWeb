using System.Collections.Generic;

namespace PrWebBackend.DTOs.Quiz
{
    public class AnswerDTO
    {
        public int QuestionId { get; set; }
        public string? Answer { get; set; }
        public bool? Correct { get; set; }
        public int? SelectionId { get; set; }
        public int[] OptionIds { get; set; }
    }
    public class QuizResultRequestDTO
    {
        public int UserId { get; set; }
        public string UserUsername { get; set; }
        public int QuizId { get; set; }
        public int TimeNeededSeconds { get; set; }
        public List<AnswerDTO> Answers { get; set; }
    }
}
