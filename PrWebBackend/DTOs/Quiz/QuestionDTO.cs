using PrWebBackend.Models.Quiz;
using System;
using System.Collections.Generic;
using Ubiety.Dns.Core;

namespace PrWebBackend.DTOs.Quiz
{
    public class QuestionDTO
    {
        private int id;
        private string text;
        private string theme;
        private string type;
        private string answer;
        private bool isCorrect;
        private List<QuestionOptionDTO> options = new List<QuestionOptionDTO>();

        public QuestionDTO(Models.Quiz.Question question)
        {
            Id = question.Id;
            Text = question.Text;
            Theme = question.Theme;
            Type = question.GetQuestionType().ToString();

            if(QuestionType.FILL_IN == question.GetQuestionType())
            {
                Answer = ((FillQuestion)question).Answer;
                IsCorrect = false;
                options = null;
            }
            else if(QuestionType.TRUE_FALSE == question.GetQuestionType())
            {
                Answer = "";
                IsCorrect = ((TrueFalseQuestion)question).IsCorrect;
                options = null;
            }
            else if(QuestionType.SINGLE_CHOICE == question.GetQuestionType())
            {
                Answer = "";
                IsCorrect = false;
                SingleChoiceQuestion singleChoiceQuestion = (SingleChoiceQuestion)question;
                foreach(QuestionOption option in singleChoiceQuestion.Options)
                {
                    options.Add(new QuestionOptionDTO(option));
                }
            }
            else if(QuestionType.MULTI_CHOICE == question.GetQuestionType())
            {
                Answer = "";
                IsCorrect = false;
                MultiChoiceQuestion multiChoiceQuestion = (MultiChoiceQuestion)question;
                foreach (QuestionOption option in multiChoiceQuestion.Options)
                {
                    options.Add(new QuestionOptionDTO(option));
                }
            }
            else
            {
                throw new ArgumentNullException();
            }

        }

        public int Id { get => id; set => id = value; }
        public string Text { get => text; set => text = value; }
        public string Type { get => type; set => type = value; }
        public string Answer { get => answer; set => answer = value; }
        public bool IsCorrect { get => isCorrect; set => isCorrect = value; }
        public List<QuestionOptionDTO> Options { get => options; set => options = value; }
        public string Theme { get => theme; set => theme = value; }
    }
}
