using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplicacaoEscolas.WebApi.Hosting.Atributos;

namespace AplicacaoEscolas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnforceHttpsController : ControllerBase
    {

        [RequireHttps]
        [HttpGet("Sim")]
        public IActionResult RecuperarHttps()
        {
            return Ok("Tudo Certo com https");
        }

        [HttpGet("Nao")]
        public IActionResult Recuperar()
        {
            return Ok("Tudo Certo");
        }

        [RequireHttpsOrClose]
        [HttpGet("ComErro")]
        public IActionResult RecuperarSeguro()
        {
            return Ok("Tudo Certo Seguro");
        }
    }
}
