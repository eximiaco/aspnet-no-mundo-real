using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplicacaoEscolas.WebApi.Models;

namespace AplicacaoEscolas.WebApi.Infraestrutura
{
    public sealed class TurmasRepositorio
    {
        private readonly List<Turma> _turmas;

        public TurmasRepositorio()
        {
            _turmas = new List<Turma>();
        }

        public void Inserir(Turma turma)
        {
            _turmas.Add(turma);
        }

        public IEnumerable<Turma> RecuperarTodos()
        {
            return _turmas;
        }
    }
}
