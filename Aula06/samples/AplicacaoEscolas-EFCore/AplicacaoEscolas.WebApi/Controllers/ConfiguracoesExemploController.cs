using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplicacaoEscolas.WebApi.Hosting.Configuration;
using Microsoft.Extensions.Configuration;

namespace AplicacaoEscolas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracoesExemploController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MatriculasOptions _matriculasOptions;

        public ConfiguracoesExemploController(
            IConfiguration configuration,
            MatriculasOptions matriculasOptions)
        {
            _configuration = configuration;
            _matriculasOptions = matriculasOptions;
        }

        [HttpGet]
        public IActionResult Recuperar()
        {
            var minhaChave = _configuration["MinhaChaveConfiguracao"];
            var usuarioFuncao = _configuration["Usuario:Funcao"];
            var usuarioNome = _configuration["Usuario:Nome"];

            return Ok(new
            {
                minhaChave,
                usuarioNome,
                usuarioFuncao
            });
        }

        [HttpGet("Matriculas")]
        public IActionResult RecuperarMatriculas()
        {
            return Ok(_matriculasOptions);
        }
    }
}
