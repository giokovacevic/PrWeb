using Microsoft.VisualBasic;
using Org.BouncyCastle.Security;
using PrWebBackend.DTOs.Quiz;
using PrWebBackend.Models.Quiz;
using PrWebBackend.Repositories.Interfaces;
using PrWebBackend.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace PrWebBackend.Services.Implementations
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }
        public List<QuizDTO> GetAllQuizzes()
        {
            List<Quiz> quizes = _quizRepository.ReadAllQuizzes();
            List<QuizDTO> quizDTOs = new List<QuizDTO>();
            foreach(Quiz quiz in quizes)
            {
                if (quiz != null) quizDTOs.Add(new QuizDTO(quiz));
            }
            return quizDTOs;
        }

        public QuizDTO GetQuizById(int quizId)
        {
            Quiz quiz = _quizRepository.ReadQuizById(quizId);
            QuizDTO quizDTO = null;
            if (quiz != null) quizDTO = new QuizDTO(quiz);
            return quizDTO;
        }

        public QuizResultDTO AddQuizResult(QuizResultRequestDTO quizResultRequestDTO) // TODO:
        {
            Quiz quiz = _quizRepository.ReadQuizById(quizResultRequestDTO.QuizId);
            if (quiz == null) return null;
            QuizResult quizResult = evaluateQuizResult(quiz, quizResultRequestDTO);
            _quizRepository.CreateQuizResult(quizResult);
            
            return new QuizResultDTO(quizResult);
        }

        public List<QuizResultDTO> GetAllQuizResultsByUserId(int userId)
        {
            List<QuizResult> quizResults = _quizRepository.ReadAllQuizResultsByUserId(userId);
            List<QuizResultDTO> quizResultDTOs = new List<QuizResultDTO>();
            foreach (QuizResult quizResult in quizResults)
            {
                if (quizResult != null) quizResultDTOs.Add(new QuizResultDTO(quizResult));
            }
            return quizResultDTOs;
        }

        public List<QuizResultDTO> GetAllQuizResults() // TODO:
        {
            return null;
        }

        private QuizResult evaluateQuizResult(Quiz quiz, QuizResultRequestDTO quizResultRequest)
        {
            int points = 0;
            int correctAnswers = 0;
            foreach(Question question in quiz.Questions)
            {
                AnswerDTO answer = GetAnswerDTOByQuestionId(quizResultRequest.Answers, question.Id);
                switch (question.GetQuestionType())
                {
                    case QuestionType.FILL_IN:
                        if(answer.Answer!=null && answer.Answer.ToLower().Equals( ((FillQuestion)(question)).Answer.ToLower() ))
                        {
                            points += 5;
                            correctAnswers += 1;
                        }
                        break;
                    case QuestionType.TRUE_FALSE:
                        if(answer.Correct != null)
                        {
                            if(answer.Correct == ((TrueFalseQuestion)(question)).IsCorrect)
                            {
                                points += 5;
                                correctAnswers += 1;
                            }
                            else
                            {
                                points -= 3;
                            }
                        }
                        break;
                    case QuestionType.SINGLE_CHOICE:
                        bool wrong = true;
                        if (answer.SelectionId != null)
                        {
                            SingleChoiceQuestion q = (SingleChoiceQuestion)question;
                            foreach(QuestionOption option in q.Options)
                            {
                                if(option.Id == answer.SelectionId && option.IsCorrect)
                                {
                                    points += 5;
                                    correctAnswers += 1;
                                    wrong = false;
                                    break;
                                }
                            }
                        }
                        if (wrong && answer.SelectionId != null) points -= 2;
                        break;
                    case QuestionType.MULTI_CHOICE:
                        int totalRight = 0;
                        int matched = 0;
                        if (answer.OptionIds != null)
                        {
                            MultiChoiceQuestion q = (MultiChoiceQuestion)question;
                            foreach (QuestionOption option in q.Options)
                            {
                                if (option.IsCorrect) totalRight++;

                                foreach(int optionId in answer.OptionIds)
                                {
                                    if(option.Id == optionId && option.IsCorrect)
                                    {
                                        matched++;
                                    }
                                }
                            }

                            if(matched == totalRight && answer.OptionIds.Length == matched)
                            {
                                points += 5;
                                correctAnswers += 1;
                            }
                            else if(answer.OptionIds.Length > 0)
                            {
                                points -= 2;
                            }
                        }
                        break;
                    default:
                        throw new System.Exception("Question for evaluation is missing QuestionType");
                }
            }
            return new QuizResult(0, quiz, quiz.Id, quizResultRequest.UserUsername, quizResultRequest.UserId, DateTime.Now, quizResultRequest.TimeNeededSeconds, correctAnswers, points);
        }
        
        private AnswerDTO GetAnswerDTOByQuestionId(List<AnswerDTO> answerDTOs, int questionId)
        {
            foreach(AnswerDTO answerDTO in answerDTOs)
            {
                if(answerDTO.QuestionId == questionId)
                {
                    return answerDTO;
                }
            }
            return null;
        }
    }
}
