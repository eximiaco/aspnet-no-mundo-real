using System;
using AplicacaoEscolas.WebApi.Dominio;
using AplicacaoEscolas.WebApi.Infraestrutura;
using AplicacaoEscolas.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AplicacaoEscolas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly AlunosRepositorio _alunosRepositorio;

        public AlunosController(AlunosRepositorio alunosRepositorio)
        {
            _alunosRepositorio = alunosRepositorio;
        }
        
        [HttpPost]
        public IActionResult Incluir([FromBody] NovoAlunoInputModel inputModel)
        {
            var aluno = Aluno.Criar(inputModel.Nome, inputModel.DataNascimento);
            if (aluno.IsFailure)
                return BadRequest(aluno.Error);
            _alunosRepositorio.Inserir(aluno.Value);
            _alunosRepositorio.Commit();
            return CreatedAtAction(nameof(RecuperarPorId), new { id = aluno.Value.Id }, aluno.Value.Id);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarPorId(Guid id)
        {
            return Ok(_alunosRepositorio.RecuperarPorId(id));
        }
        
    }
}