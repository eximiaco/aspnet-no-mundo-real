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
        private readonly TurmasRepositorioSqlServer _turmasRepositorioSqlServer;

        public TurmasController(TurmasRepositorioSqlServer turmasRepositorioSqlServer)
        {
            _turmasRepositorioSqlServer = turmasRepositorioSqlServer;
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody]Turma turma)
        {
            turma.Id = Guid.NewGuid();
            _turmasRepositorioSqlServer.Inserir(turma);

            return Ok(turma);
        }

        [HttpGet]
        public IActionResult RecuperarTodos([FromQuery]string descricao)
        {
            return Ok(_turmasRepositorioSqlServer.RecuperarTodos());
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarPorId(string id)
        {

            var guid = Guid.Parse(id);

            return Ok(guid);
        }


    }
}
