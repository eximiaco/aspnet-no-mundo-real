using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplicacaoEscolas.WebApi.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AplicacaoEscolas.WebApi.Infraestrutura
{
    public sealed class TurmasRepositorioSqlServer 
    {
        private readonly IConfiguration _configuracao;


        public TurmasRepositorioSqlServer(IConfiguration configuracao)
        {
            _configuracao = configuracao;
        }

        public void Inserir(Turma turma)
        {
            using (SqlConnection connection = new SqlConnection(_configuracao.GetConnectionString("Escolas")))
            {
                var comando = new SqlCommand(
                    $"INSERT INTO Turmas (Id, Descricao) VALUES ('{turma.Id}','{turma.Descricao}')", connection);
                connection.Open();
                var resutlado = comando.ExecuteNonQuery();
            }
        }

        public IEnumerable<Turma> RecuperarTodos()
        {
            using (SqlConnection connection = new SqlConnection(_configuracao.GetConnectionString("Escolas")))
            {
                var comando = new SqlCommand(@"SELECT Turma.Id, 
                                                                Turmas.Descricao                                                 
                                                        FROM Turmas"
                    , connection);
                connection.Open();
                var reader = comando.ExecuteReader();
                var listaTurmas = new List<Turma>();
                while (reader.Read())
                {
                    
                    listaTurmas.Add(
                        new Turma()
                    {
                        Id = Guid.Parse(reader.GetString(0)),
                        Descricao = reader.GetString(1)
                    });
                }
                return listaTurmas;
            }
        }
    }
}
