using AplicacaoEscolas.WebApi.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AplicacaoEscolas.WebApi.Infraestrutura.EntityConfigurations
{
    public sealed class AgendaTypeConfiguration : IEntityTypeConfiguration<Agenda>
    {
        public void Configure(EntityTypeBuilder<Agenda> builder)
        {
            builder.ToTable("TurmasAgenda", "Matriculas");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.DiaSemana).HasConversion(new EnumToStringConverter<EDiaSemana>())
                .HasColumnType("varchar(20)");
            builder.Property(c => c.HoraInicial).HasConversion(EFConversores.HorarioConverter)
                .HasColumnType("varchar(5)");
            builder.Property(c => c.HoraFinal).HasConversion(EFConversores.HorarioConverter)
                .HasColumnType("varchar(5)");
        }
    }
}