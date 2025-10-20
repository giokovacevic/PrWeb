using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrWebBackend.DTOs.Quiz;
using PrWebBackend.Services.Implementations;
using PrWebBackend.Services.Interfaces;
using System.Collections.Generic;

namespace PrWebBackend.Controllers
{
    [ApiController]
    [Route("quizzes")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet("all")]
        [Authorize(Roles = "player")]
        public List<QuizDTO> GetAllQuizzes() // TODO:
        {
            return _quizService.GetAllQuizzes();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "player")]
        public IActionResult GetQuizById(int id)
        {
            QuizDTO response = _quizService.GetQuizById(id);
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPost("quiz-result")]
        [Authorize(Roles = "player")]
        public IActionResult PostQuizResults([FromBody] QuizResultRequestDTO quizResultRequestDTO) // TODO:
        {
            QuizResultDTO quizResultDTO = _quizService.AddQuizResult(quizResultRequestDTO);
            if (quizResultDTO == null) return BadRequest();
            return Ok(quizResultDTO);
        }

        [HttpGet("quiz-result/{id}")]
        [Authorize(Roles = "player")]
        public List<QuizResultDTO> GetAllQuizResultsByUserId(int id) // TODO:
        {
            List<QuizResultDTO> quizResultDTOs = _quizService.GetAllQuizResultsByUserId(id);
            return quizResultDTOs;
        }
    }
}
