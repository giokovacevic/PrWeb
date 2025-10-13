using Microsoft.AspNetCore.Mvc;
using PrWebBackend.DTOs;
using PrWebBackend.Services.Implementations;
using System.Collections.Generic;

namespace PrWebBackend.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            this._personService = personService;
        }

        [HttpGet("all")]
        public List<PersonDTO> GetAll()
        {
            return _personService.GetAll();
        }
    }
}

/*
 
private readonly ILogger<WeatherForecastController> _logger;

public WeatherForecastController(ILogger<WeatherForecastController> logger)
{
    _logger = logger;
}
 
 */
