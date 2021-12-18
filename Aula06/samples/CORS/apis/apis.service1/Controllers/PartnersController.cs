using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace apis.service1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartnersController : ControllerBase
    {
        private readonly ILogger<PartnersController> _logger;

        public PartnersController(
            ILogger<PartnersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new[]
            {
                new 
                    {
                        Id = 1,
                        Name = "Partner 1"
                    },
                new
                    {
                        Id = 2,
                        Name = "Partner 2"
                    }
            });
        }
    }
}