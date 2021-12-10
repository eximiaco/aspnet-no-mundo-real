using System;
using System.Data;
using Dapper;

namespace AplicacaoEscolas.WebApi.Infraestrutura.Mappers
{
    public sealed class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override void SetValue(IDbDataParameter parameter, Guid value)
        {
            parameter.Value = value.ToString();
        }

        public override Guid Parse(object value)
        {
            return new Guid((string) value);
        }
    }
}