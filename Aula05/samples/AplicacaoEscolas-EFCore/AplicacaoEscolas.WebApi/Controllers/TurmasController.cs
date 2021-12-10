using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoEscolas.WebApi.Infraestrutura;
using AplicacaoEscolas.WebApi.Dominio;
using AplicacaoEscolas.WebApi.Models;
using Dapper;

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

            return CreatedAtAction(nameof(RecuperarPorId), new { id = turma.Value.Id }, turma.Value.Id);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(string id, [FromBody]AlterarTurmaInputModel turmaInputModel)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("Id inválido");
            var turma = _turmasRepositorio.RecuperarPorId(guid);
            if (turma == null)
                return NotFound();
            turma.LimparAgendas();
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
                    var agenda = new Agenda(guidAgenda, (EDiaSemana)agendaInput.DiaSemana, horaInicial.Value, horaFinal.Value);
                    turma.AdicionarAgenda(agenda);
                }
            }
            
            _turmasRepositorio.Alterar(turma);

            return Ok(turma);
        }
        
        [HttpGet]
        public IActionResult RecuperarTodos()
        {
            return Ok(_turmasRepositorio.RecuperarTodos());
        }
        
        [HttpGet("{id}")]
        public IActionResult RecuperarPorId(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("Id inválido");
            var turma = _turmasRepositorio.RecuperarPorId(guid);
            if (turma == null)
                return NotFound();
            return Ok(turma);
        }
    }
}
