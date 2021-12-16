using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoEscolas.WebApi.Infraestrutura;
using AplicacaoEscolas.WebApi.Dominio;
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
        public async Task<IActionResult> CadastrarAsync([FromBody]NovaTurmaInputModel turmaInputModel, CancellationToken cancellationToken)
        {
            var turma = Turma.Criar(turmaInputModel.Descricao, turmaInputModel.Modalidade, turmaInputModel.QuantidadeVagas);
            if(turma.IsFailure)
                return BadRequest(turma.Error);
            foreach (var agendaInput in turmaInputModel.Agenda)
            {
                var horaInicial = Horario.Criar(agendaInput.HoraInicial);
                if(horaInicial.IsFailure)
                    return BadRequest(horaInicial.Error);
                var horaFinal = Horario.Criar(agendaInput.HoraFinal);
                if(horaFinal.IsFailure)
                    return BadRequest(horaFinal.Error);
                turma.Value.AdicionarAgenda((EDiaSemana)agendaInput.DiaSemana, horaInicial.Value, horaFinal.Value);
            }
            await _turmasRepositorio.InserirAsync(turma.Value, cancellationToken);
            await _turmasRepositorio.CommitAsync(cancellationToken);
            return CreatedAtAction("RecuperarPorId", new { id = turma.Value.Id }, turma.Value.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(string id, [FromBody]AlterarTurmaInputModel turmaInputModel, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("Id inválido");
            var turma = await _turmasRepositorio.RecuperarPorIdAsync(guid, cancellationToken);
            if (turma == null)
                return NotFound();

            var agendasExistentes =
                turma.Agenda
                    .Where(c => turmaInputModel.Agenda.Any(input => input.Id == c.Id.ToString()))
                    .Select(c=> c.Id);

            var agendasParaExcluir = turma.Agenda
                .Where(c => agendasExistentes.Any(id => id != c.Id))
                .Select(c=> c.Id);

            turma.RemoverAgendas(agendasParaExcluir);
            
            foreach (var agendaInput in turmaInputModel.Agenda)
            {
                var horaInicial = Horario.Criar(agendaInput.HoraInicial);
                if(horaInicial.IsFailure)
                    return BadRequest(horaInicial.Error);
                var horaFinal = Horario.Criar(agendaInput.HoraFinal);
                if(horaFinal.IsFailure)
                    return BadRequest(horaFinal.Error);
                if (string.IsNullOrEmpty(agendaInput.Id))
                {
                    turma.AdicionarAgenda((EDiaSemana)agendaInput.DiaSemana, horaInicial.Value, horaFinal.Value);
                }
                else
                {
                    if (!Guid.TryParse(agendaInput.Id, out var guidAgenda))
                        return BadRequest("Id da agenda inválido");
                    turma.AtualizarAgenda(guidAgenda,  (EDiaSemana)agendaInput.DiaSemana, horaInicial.Value, horaFinal.Value);
                }
            }
            
            _turmasRepositorio.Alterar(turma);
            await _turmasRepositorio.CommitAsync(cancellationToken);
            
            return Ok(turma);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarPorIdAsync(string id, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("Id inválido");
            var turma = await _turmasRepositorio.RecuperarPorIdAsync(guid, cancellationToken);
            if (turma == null)
                return NotFound();
            return Ok(turma);
        }
    }
}
