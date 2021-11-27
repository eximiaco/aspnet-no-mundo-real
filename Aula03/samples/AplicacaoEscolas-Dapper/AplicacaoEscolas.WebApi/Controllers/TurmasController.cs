using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplicacaoEscolas.WebApi.Infraestrutura;
using AplicacaoEscolas.WebApi.Models;

namespace AplicacaoEscolas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmasController : ControllerBase
    {
        private readonly TurmasRepositorio _turmasRepositorio;

        public TurmasController(TurmasRepositorio turmasRepositorio)
        {
            _turmasRepositorio = turmasRepositorio;
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody]Turma turma)
        {
            turma.Id = Guid.NewGuid().ToString();
            _turmasRepositorio.Inserir(turma);

            return Ok(turma);
        }

        [HttpGet]
        public IActionResult RecuperarTodos()
        {

            return Ok(_turmasRepositorio.RecuperarTodos());
        }
    }
}
