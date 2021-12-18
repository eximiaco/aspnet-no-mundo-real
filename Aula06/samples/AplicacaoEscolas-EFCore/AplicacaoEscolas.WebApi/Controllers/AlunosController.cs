using System;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoEscolas.WebApi.Dominio;
using AplicacaoEscolas.WebApi.Infraestrutura;
using AplicacaoEscolas.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AplicacaoEscolas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly ILogger<AlunosController> _logger;
        private readonly AlunosRepositorio _alunosRepositorio;

        public AlunosController(
            ILogger<AlunosController> logger,
            AlunosRepositorio alunosRepositorio)
        {
            _logger = logger;
            _alunosRepositorio = alunosRepositorio;
        }
        
        [HttpPost]
        public async Task<IActionResult> IncluirAsync([FromBody] NovoAlunoInputModel inputModel, CancellationToken cancellationToken)
        {

            var aluno = Aluno.Criar(inputModel.Nome, inputModel.DataNascimento, (EGenero)inputModel.Genero, 
                new EnderecoCompleto(inputModel.EnderecoResidencial.Rua, inputModel.EnderecoResidencial.Numero,
                    inputModel.EnderecoResidencial.Complemento, inputModel.EnderecoResidencial.Bairro,
                    inputModel.EnderecoResidencial.Cidade, inputModel.EnderecoResidencial.Cep, 
                    inputModel.EnderecoResidencial.UF, inputModel.EnderecoResidencial.Pais));
            if (aluno.IsFailure)
                return BadRequest(aluno.Error);

            _logger.LogInformation("Aluno {aluno} criado em memória", aluno.Value.Id);

            await _alunosRepositorio.InserirAsync(aluno.Value, cancellationToken);
            await _alunosRepositorio.CommitAsync(cancellationToken);
            return CreatedAtAction(nameof(RecuperarPorId), new { id = aluno.Value.Id }, aluno.Value.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarPorId(Guid id, CancellationToken cancellationToken)
        {
            var aluno = await _alunosRepositorio.RecuperarPorIdAsync(id, cancellationToken);
            
            return Ok(aluno);
        }
        
    }
}