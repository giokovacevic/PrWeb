using MySql.Data.MySqlClient;
using PrWebBackend.Models;
using PrWebBackend.Models.NamespaceUser;
using PrWebBackend.Models.Quiz;
using PrWebBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrWebBackend.Repositories.Implementations
{
    public class QuizRepository : IQuizRepository
    {
        private const string DifficultyId = "DifficultyId";
        private const string DifficultyValue = "DifficultyValue";
        private const string QuestionId = "QuestionId";
        private const string QuestionType = "QuestionType";
        private const string QuestionText = "QuestionText";
        private const string QuestionTheme = "QuestionTheme";
        private const string QuestionAnswer = "QuestionAnswer";
        private const string QuestionCorrect = "QuestionCorrect";
        private const string QuestionOptionId = "QuestionOptionId";
        private const string QuestionOptionText = "QuestionOptionText";
        private const string QuestionOptionCorrect = "QuestionOptionCorrect";
        private const string UserId = "UserId";
        private const string UserUsername = "UserUsername";
        private const string QuizResultDate = "QuizResultDate";
        private const string QuizResultId = "QuizResultId";
        private const string QuizResultTimeNeeded = "QuizResultTimeNeeded";
        private const string QuizResultCorrectAmount = "QuizResultCorrectAmount";
        private const string QuizResultPoints = "QuizResultPoints";

        private readonly string _connectionString;

        public static readonly Dictionary<string, string> Columns = new Dictionary<string, string>()
        {
            {nameof(Quiz.Id), "quiz_id" },
            {nameof(Quiz.Name), "quiz_name" },
            {nameof(Quiz.TimeSeconds), "quiz_time_in_seconds" },
            {nameof(Quiz.Description), "quiz_description" },
            
            {DifficultyId, "difficulty_id" },
            {DifficultyValue, "difficulty_value" },
            
            {QuestionId, "question_id" },
            {QuestionType, "question_type" },
            {QuestionText, "question_text" },
            {QuestionTheme, "question_theme" },
            {QuestionAnswer, "question_answer" },
            {QuestionCorrect, "question_correct" },
            
            {QuestionOptionId, "questionoption_id" },
            {QuestionOptionText, "questionoption_text" },
            {QuestionOptionCorrect, "questionoption_correct" },
           
            {UserId, "user_id"},
            {UserUsername, "user_username"},

            {QuizResultId, "quizresult_id" },
            {QuizResultDate, "quizresult_date" },
            {QuizResultTimeNeeded, "quizresult_time_needed" },
            {QuizResultCorrectAmount, "quizresult_correct_amount" },
            {QuizResultPoints, "quizresult_points" },
        };

        public QuizRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Quiz> ReadAllQuizzes()
        {
            Dictionary<int, Quiz> quizDictionary = new Dictionary<int, Quiz>();

            string quizAlias = "qz";
            string difficultyAlias = "d";
            string questionAlias = "q";
            string quizQuestionAlias = "qq";
            string questionOptionAlias = "qo";

            string query = $"SELECT {quizAlias}.{Columns[nameof(Quiz.Id)]} as {Columns[nameof(Quiz.Id)]}, {Columns[nameof(Quiz.Name)]}, {Columns[nameof(Quiz.TimeSeconds)]}, {Columns[nameof(Quiz.Description)]}, {difficultyAlias}.{Columns[DifficultyId]} as {Columns[DifficultyId]}, {Columns[DifficultyValue]}, {questionAlias}.{Columns[QuestionId]} as {Columns[QuestionId]}, {Columns[QuestionType]}, {Columns[QuestionText]}, {Columns[QuestionTheme]}, {Columns[QuestionAnswer]}, {Columns[QuestionCorrect]}, {Columns[QuestionOptionId]}, {Columns[QuestionOptionText]}, {Columns[QuestionOptionCorrect]} FROM Quiz {quizAlias} INNER JOIN Difficulty {difficultyAlias} on {quizAlias}.{Columns[DifficultyId]} = {difficultyAlias}.{Columns[DifficultyId]} JOIN QuizQuestion {quizQuestionAlias} on {quizAlias}.{Columns[nameof(Quiz.Id)]} = {quizQuestionAlias}.{Columns[nameof(Quiz.Id)]} JOIN Question {questionAlias} on {quizQuestionAlias}.{Columns[QuestionId]} = {questionAlias}.{Columns[QuestionId]} LEFT JOIN QuestionOption {questionOptionAlias} on {questionAlias}.{Columns[QuestionId]} = {questionOptionAlias}.{Columns[QuestionId]};";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Quiz quiz = null;
                            int quizId = reader.GetInt32(reader.GetOrdinal(Columns[nameof(Quiz.Id)]));
                            
                            if (!quizDictionary.ContainsKey(quizId))
                            {
                                quiz = ExtractQuizFromReader(reader);
                                quizDictionary[quizId] = quiz;
                            }
                            else
                            {
                                quiz = quizDictionary[quizId];
                            }

                            int questionId = reader.GetInt32(reader.GetOrdinal(Columns[QuestionId]));
                            Question question = GetQuestion(quiz, questionId);
                            if (question == null)
                            {
                                question = ExtractQuestionFromReader(reader);
                                quiz.Questions.Add(question);
                            }

                            int optionId = reader.IsDBNull(reader.GetOrdinal(Columns[QuestionOptionId])) ? -1 : reader.GetInt32(reader.GetOrdinal(Columns[QuestionOptionId]));

                            if (optionId != -1)
                            {
                                if (question.GetQuestionType() == Models.Quiz.QuestionType.SINGLE_CHOICE)
                                {
                                    QuestionOption option = GetOption((SingleChoiceQuestion)question, optionId);
                                    if (option == null)
                                    {
                                        option = ExtractOptionFromReader(reader);
                                        ((SingleChoiceQuestion)question).Options.Add(option);
                                    }
                                }
                                else if (question.GetQuestionType() == Models.Quiz.QuestionType.MULTI_CHOICE)
                                {
                                    QuestionOption option = GetOption((MultiChoiceQuestion)question, optionId);
                                    if (option == null)
                                    {
                                        option = ExtractOptionFromReader(reader);
                                        ((MultiChoiceQuestion)question).Options.Add(option);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return quizDictionary.Values.ToList<Quiz>();
        }

        public Quiz ReadQuizById(int id)
        {
            Quiz quiz = null;

            string quizAlias = "qz";
            string difficultyAlias = "d";
            string questionAlias = "q";
            string quizQuestionAlias = "qq";
            string questionOptionAlias = "qo";

            string query = $"SELECT {quizAlias}.{Columns[nameof(Quiz.Id)]} as {Columns[nameof(Quiz.Id)]}, {Columns[nameof(Quiz.Name)]}, {Columns[nameof(Quiz.TimeSeconds)]}, {Columns[nameof(Quiz.Description)]}, {difficultyAlias}.{Columns[DifficultyId]} as {Columns[DifficultyId]}, {Columns[DifficultyValue]}, {questionAlias}.{Columns[QuestionId]} as {Columns[QuestionId]}, {Columns[QuestionType]}, {Columns[QuestionText]}, {Columns[QuestionTheme]}, {Columns[QuestionAnswer]}, {Columns[QuestionCorrect]}, {Columns[QuestionOptionId]}, {Columns[QuestionOptionText]}, {Columns[QuestionOptionCorrect]} FROM Quiz {quizAlias} INNER JOIN Difficulty {difficultyAlias} on {quizAlias}.{Columns[DifficultyId]} = {difficultyAlias}.{Columns[DifficultyId]} JOIN QuizQuestion {quizQuestionAlias} on {quizAlias}.{Columns[nameof(Quiz.Id)]} = {quizQuestionAlias}.{Columns[nameof(Quiz.Id)]} JOIN Question {questionAlias} on {quizQuestionAlias}.{Columns[QuestionId]} = {questionAlias}.{Columns[QuestionId]} LEFT JOIN QuestionOption {questionOptionAlias} on {questionAlias}.{Columns[QuestionId]} = {questionOptionAlias}.{Columns[QuestionId]} WHERE {quizAlias}.{Columns[nameof(Quiz.Id)]} = @id;";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())    
                        {
                            if(quiz == null)
                            {
                                quiz = ExtractQuizFromReader(reader);
                            }

                            int questionId = reader.GetInt32(reader.GetOrdinal(Columns[QuestionId]));
                            Question question = GetQuestion(quiz, questionId);
                            if(question == null)
                            {
                                question = ExtractQuestionFromReader(reader);
                                quiz.Questions.Add(question);
                            }

                            int optionId = reader.IsDBNull(reader.GetOrdinal(Columns[QuestionOptionId])) ? -1 : reader.GetInt32(reader.GetOrdinal(Columns[QuestionOptionId]));
                           
                            if(optionId != -1)
                            {
                                if (question.GetQuestionType() == Models.Quiz.QuestionType.SINGLE_CHOICE)
                                {
                                    QuestionOption option = GetOption((SingleChoiceQuestion)question, optionId);
                                    if (option == null)
                                    {
                                        option = ExtractOptionFromReader(reader);
                                        ((SingleChoiceQuestion)question).Options.Add(option);
                                    }
                                }
                                else if (question.GetQuestionType() == Models.Quiz.QuestionType.MULTI_CHOICE)
                                {
                                    QuestionOption option = GetOption((MultiChoiceQuestion)question, optionId);
                                    if (option == null)
                                    {
                                        option = ExtractOptionFromReader(reader);
                                        ((MultiChoiceQuestion)question).Options.Add(option);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return quiz;
        }

        public void CreateQuizResult(QuizResult quizResult)
        {
            string query = $"INSERT INTO QuizResult({Columns[nameof(Quiz.Id)]}, {Columns[UserId]}, {Columns[QuizResultDate]}, {Columns[QuizResultTimeNeeded]}, {Columns[QuizResultCorrectAmount]}, {Columns[QuizResultPoints]}) VALUES (@param1, @param2, @param3, @param4, @param5, @param6);";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@param1", quizResult.Quiz.Id); // + quiz_name + quiz_difficulty
                    command.Parameters.AddWithValue("@param2", quizResult.UserId); // + user_name
                    command.Parameters.AddWithValue("@param3", quizResult.Datetime);
                    command.Parameters.AddWithValue("@param4", quizResult.TimeNeededSeconds);
                    command.Parameters.AddWithValue("@param5", quizResult.CorrectAnswers); // preko counta max
                    command.Parameters.AddWithValue("@param6", quizResult.Points); // count * 5

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<QuizResult> ReadAllQuizResultsByUserId(int userId)
        {
            List<QuizResult> quizResults = new List<QuizResult>();

            string quizResultAlias = "qr";
            string userAlias = "u";

            string query = $"SELECT {Columns[QuizResultId]}, {Columns[nameof(Quiz.Id)]}, {quizResultAlias}.{Columns[UserId]} as {Columns[UserId]}, {Columns[UserUsername]}, {Columns[QuizResultDate]}, {Columns[QuizResultTimeNeeded]}, {Columns[QuizResultCorrectAmount]}, {Columns[QuizResultPoints]} from QuizResult {quizResultAlias} INNER JOIN User {userAlias} on {quizResultAlias}.{Columns[UserId]} = {userAlias}.{Columns[UserId]}  WHERE {quizResultAlias}.{Columns[UserId]} = @userId;";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            quizResults.Add(ExtractQuizResultFromReader(reader));
                        }
                    }
                }
            }

            foreach(QuizResult quizResult in quizResults)
            {
                quizResult.Quiz = ReadQuizById(quizResult.QuizId);
            }

            return quizResults;
        }


        private Quiz ExtractQuizFromReader(MySqlDataReader reader)
        {
            int quizId = reader.GetInt32(reader.GetOrdinal(Columns[nameof(Quiz.Id)]));
            string quizName = reader.GetString(reader.GetOrdinal(Columns[nameof(Quiz.Name)]));
            int quizTimeSeconds = reader.GetInt32(reader.GetOrdinal(Columns[nameof(Quiz.TimeSeconds)]));
            string quizDescription = reader.GetString(reader.GetOrdinal(Columns[nameof(Quiz.Description)]));
            int difficultyId = reader.GetInt32(reader.GetOrdinal(Columns[DifficultyId]));
            string difficultyValue = reader.GetString(reader.GetOrdinal(Columns[DifficultyValue]));
            return new Quiz(quizId, quizName, quizTimeSeconds, new QuizDifficulty(difficultyId, difficultyValue), quizDescription);
        }

        private Models.Quiz.Question ExtractQuestionFromReader(MySqlDataReader reader)
        {
            int questionId = reader.GetInt32(reader.GetOrdinal(Columns[QuestionId]));
            string questionType = reader.GetString(reader.GetOrdinal(Columns[QuestionType]));
            string questionText = reader.GetString(reader.GetOrdinal(Columns[QuestionText]));
            string questionTheme = reader.GetString(reader.GetOrdinal(Columns[QuestionTheme]));

            if(questionType == Models.Quiz.QuestionType.FILL_IN.ToString())
            {
                string questionAnswer = reader.GetString(reader.GetOrdinal(Columns[QuestionAnswer]));
                return new FillQuestion(questionId, questionText, questionTheme, questionAnswer);
            }

            if(questionType == Models.Quiz.QuestionType.TRUE_FALSE.ToString())
            {
                bool questionCorrect = reader.GetBoolean(reader.GetOrdinal(Columns[QuestionCorrect]));
                return new TrueFalseQuestion(questionId, questionText, questionTheme, questionCorrect);
            }
            
            if(questionType == Models.Quiz.QuestionType.SINGLE_CHOICE.ToString())
            {
                return new SingleChoiceQuestion(questionId, questionText, questionTheme);
            }

            if (questionType == Models.Quiz.QuestionType.MULTI_CHOICE.ToString())
            {
                return new MultiChoiceQuestion(questionId, questionText, questionTheme);
            }

            return null;
        }

        private QuestionOption ExtractOptionFromReader(MySqlDataReader reader)
        {
            int optionId = reader.GetInt32(reader.GetOrdinal(Columns[QuestionOptionId]));
            string optionText = reader.GetString(reader.GetOrdinal(Columns[QuestionOptionText]));
            bool optionCorrect = reader.GetBoolean(reader.GetOrdinal(Columns[QuestionOptionCorrect]));
            return new QuestionOption(optionId, optionText, optionCorrect);
        }

        private Question GetQuestion(Quiz quiz, int questionId)
        {
            foreach(Models.Quiz.Question question in quiz.Questions)
            {
                if (question.Id == questionId) return question;
            }
            return null;
        }

        private QuestionOption GetOption(SingleChoiceQuestion question, int optionId)
        {
            foreach (QuestionOption option in question.Options)
            {
                if (option.Id == optionId) return option;
            }
            return null;
        }

        private QuestionOption GetOption(MultiChoiceQuestion question, int optionId)
        {
            foreach (QuestionOption option in question.Options)
            {
                if (option.Id == optionId) return option;
            }
            return null;
        }

        private QuizResult ExtractQuizResultFromReader(MySqlDataReader reader)
        {
            int id = reader.GetInt32(reader.GetOrdinal(Columns[nameof(QuizResultId)]));
            int quizId = reader.GetInt32(reader.GetOrdinal(Columns[nameof(Quiz.Id)]));
            int userId = reader.GetInt32(reader.GetOrdinal(Columns[UserId]));
            string userUsername = reader.GetString(reader.GetOrdinal(Columns[UserUsername]));
            DateTime date = reader.GetDateTime(reader.GetOrdinal(Columns[QuizResultDate]));
            int timeNeeded = reader.GetInt32(reader.GetOrdinal(Columns[QuizResultTimeNeeded]));
            int correctAmount = reader.GetInt32(reader.GetOrdinal(Columns[QuizResultCorrectAmount]));
            int points = reader.GetInt32(reader.GetOrdinal(Columns[QuizResultPoints]));
           
            return new QuizResult(id, null, quizId, userUsername, userId, date, timeNeeded, correctAmount, points);
        }
    }
}
