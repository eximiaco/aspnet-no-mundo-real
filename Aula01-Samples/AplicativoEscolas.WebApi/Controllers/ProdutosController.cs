using System;
using System.Collections.Generic;
using System.Linq;
using AplicativoEscolas.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AplicativoEscolas.WebApi.Controllers
{
    public interface IProdutosAcessoDados
    {
        IEnumerable<string> Recuperar();
    }


    public sealed class ProdutosBancoDeDados : IProdutosAcessoDados
    {
        public IEnumerable<string> Recuperar()
        {
            return new List<string>()
            {
                "Produto 1",
                "Produto 2",
                "Produto 3"
            };
        }
    }

    public sealed class ProdutosCache : IProdutosAcessoDados
    {
        public IEnumerable<string> Recuperar()
        {
            return new List<string>()
            {
                "Produto 1",
                "Produto 2",
                "Produto 3"
            };
        }
    }

    public interface IServicoCusto
    {
        decimal CalcularCusto();
    }

    public sealed class CalculoCustoAPI : IServicoCusto
    {
        public decimal CalcularCusto()
        {
            //Ir na Api
            return 10m;
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutosAcessoDados _acessoDados;
        private readonly IServicoCusto _servicoCusto;

        public ProdutosController(
            IProdutosAcessoDados acessoDados,
            IServicoCusto servicoCusto)
        {
            _acessoDados = acessoDados;
            _servicoCusto = servicoCusto;
        }

        [HttpGet("Certo")]
        public IActionResult ListarTodos()
        {
            var produtosRecuperados = _acessoDados.Recuperar();
            foreach (var produtos in produtosRecuperados)
            {
                var custo = _servicoCusto.CalcularCusto();
            }

            return Ok(produtosRecuperados);
        }

        [HttpGet("Errado")]
        public IActionResult ListarTodosDeFormaErrada()
        {
            var acessoDados = new ProdutosBancoDeDados();
            var servicoCusto = new CalculoCustoAPI();


            var produtosRecuperados = acessoDados.Recuperar();
            foreach (var produtos in produtosRecuperados)
            {
                var custo = servicoCusto.CalcularCusto();
            }

            return Ok(produtosRecuperados);
        }
    }
}