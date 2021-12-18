using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoEscolas.WebApi.Infraestrutura;
using AplicacaoEscolas.WebApi.Dominio;
using AplicacaoEscolas.WebApi.Models;
using Microsoft.Extensions.Logging;

namespace AplicacaoEscolas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculasController : ControllerBase
    {
        private readonly AlunosRepositorio _alunosRepositorio;
        private readonly TurmasRepositorio _turmasRepositorio;
        private readonly MatriculasRepositorio _matriculasRepositorio;
        private readonly ILogger<MatriculasController> _logger;

        public MatriculasController(
            AlunosRepositorio alunosRepositorio,
            TurmasRepositorio turmasRepositorio,
            MatriculasRepositorio matriculasRepositorio,
            ILogger<MatriculasController> logger)
        {
            _alunosRepositorio = alunosRepositorio;
            _turmasRepositorio = turmasRepositorio;
            _matriculasRepositorio = matriculasRepositorio;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarAsync([FromBody] NovaMatriculaInputModel inputModel, CancellationToken cancellationToken)
        {

            if (!Guid.TryParse(inputModel.AlunoId, out var guidAluno))
                return BadRequest("Id de aluno inválido");
            if (!Guid.TryParse(inputModel.TurmaId, out var guidTurma))
                return BadRequest("Id de turma inválido");

            var aluno = await _alunosRepositorio.RecuperarPorIdAsync(guidAluno, cancellationToken);
            if (aluno == null)
                return BadRequest("Aluno não foi localizado");

            var turma = await _turmasRepositorio.RecuperarPorIdAsync(guidTurma, cancellationToken);
            if (turma == null)
                return BadRequest("Turma não foi localizada");

            var matricula = Matricula.Criar(aluno, turma);
            if (matricula.IsFailure)
                return BadRequest(matricula.Error);

            turma.AdicionarAluno();

            await _matriculasRepositorio.InserirAsync(matricula.Value, cancellationToken);
            await _matriculasRepositorio.CommitAsync(cancellationToken);

            return CreatedAtAction("RecuperarPorId", new { id = matricula.Value.Id }, matricula.Value.Id);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarPorIdAsync(string id, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("Id inválido");
            var matricula = await _matriculasRepositorio.RecuperarPorIdAsync(guid, cancellationToken);
            if (matricula == null)
                return NotFound();
            return Ok(matricula);
        }
    }
}
