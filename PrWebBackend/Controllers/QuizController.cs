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
        public List<QuizDTO> GetAllQuizzes() // TODO:
        {
            return _quizService.GetAllQuizzes();
        }

        [HttpGet("{id}")]
        public IActionResult GetQuizById(int id)
        {
            QuizDTO response = _quizService.GetQuizById(id);
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPost("quiz-result")]
        public IActionResult PostQuizResults([FromBody] QuizResultRequestDTO quizResultRequestDTO) // TODO:
        {
            QuizResultDTO quizResultDTO = _quizService.AddQuizResult(quizResultRequestDTO);
            if (quizResultDTO == null) return BadRequest();
            return Ok(quizResultDTO);
        }
    }
}
