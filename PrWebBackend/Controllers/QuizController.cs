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
        public List<QuizDTO> GetAll() // TODO:
        {
            return _quizService.GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            QuizDTO response = _quizService.GetById(id);
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPost("new-result")]
        public IActionResult PostQuizResults([FromBody] int a) // TODO:
        {
            return Ok();
        }
    }
}
