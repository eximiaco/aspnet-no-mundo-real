using AplicacaoEscolas.WebApi.Infraestrutura.Mappers;
using Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace AplicacaoEscolas.WebApi.Hosting.Extensions
{
    public static class DapperExtensions
    {
        public static IServiceCollection AddDapper(this IServiceCollection serviceCollection)
        {
            SqlMapper.AddTypeHandler(new GuidTypeHandler());
            SqlMapper.AddTypeHandler(new HorarioTypeHandler());
            return serviceCollection;
        }
    }
}