using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace apis.service2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly ILogger<InvoicesController> _logger;

        public InvoicesController(
            ILogger<InvoicesController> logger)
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
                    Name = "Invoice 1"
                },
                new
                {
                    Id = 2,
                    Name = "Invoice 2"
                }
            });
        }

    }
}