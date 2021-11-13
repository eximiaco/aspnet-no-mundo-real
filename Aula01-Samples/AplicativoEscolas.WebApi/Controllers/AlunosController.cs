using System;
using System.Collections.Generic;
using System.Linq;
using AplicativoEscolas.WebApi;
using AplicativoEscolas.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AplicativoEscolas.WebApi.Controllers
{
    public sealed class AlunoNegocio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Situacao { get; set; }
    }

    public interface IAlunosDataAccess
    {
        void Incluir(AlunoNegocio negocio);
        IEnumerable<AlunoNegocio> Listar();
    }

    public sealed class AlunosDataAccessEmMemoria : IAlunosDataAccess
    {
        private List<AlunoNegocio> _alunos;

        public AlunosDataAccessEmMemoria()
        {
            _alunos = new List<AlunoNegocio>();
        }

        public void Incluir(AlunoNegocio negocio)
        {
            var ultimoId = _alunos
                .OrderByDescending(c=>c.Id)
                .FirstOrDefault();
            if (ultimoId == null)
                negocio.Id = 1;
            else
                negocio.Id = ultimoId.Id + 1;
            _alunos.Add(negocio);
        }

        public IEnumerable<AlunoNegocio> Listar()
        {
            return _alunos;
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunosDataAccess _alunosDataAccess;

        public AlunosController(IAlunosDataAccess alunosDataAccess)
        {
            _alunosDataAccess = alunosDataAccess;
        }
        

        [HttpPost]
        public IActionResult Cadastrar([FromBody] NovoAlunoInputModel novoAluno)
        {
            var alunoNegocio = new AlunoNegocio();
            alunoNegocio.Nome = novoAluno.Nome;
            alunoNegocio.Email = novoAluno.Email;
            alunoNegocio.Situacao = "Ativo";

            _alunosDataAccess.Incluir(alunoNegocio);

            return Created("Alunos", alunoNegocio);
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var alunos = _alunosDataAccess.Listar();
            return Ok(alunos);
        }
    }
}