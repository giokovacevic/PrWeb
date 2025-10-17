using System.Collections.Generic;

namespace PrWebBackend.Models.Quiz
{
    public class SingleChoiceQuestion : Question
    {
        private List<QuestionOption> options = new List<QuestionOption>();

        public SingleChoiceQuestion(int id, string text, string theme) : base(id, text, theme)
        {

        }

        public List<QuestionOption> Options { get => options; set => options = value; }

        public override QuestionType GetQuestionType()
        {
            return QuestionType.SINGLE_CHOICE;
        }
    }
}
